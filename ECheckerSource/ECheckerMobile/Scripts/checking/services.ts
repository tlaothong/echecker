module app.checking {
    'use strict';

    //Interface form api
    interface IFormResourceClass<T> extends ng.resource.IResourceClass<ng.resource.IResource<T>> {}

    //Interface checkeds api
    interface ICheckedResourceClass<T> extends ng.resource.IResourceClass<ng.resource.IResource<T>> {
        UpdateCheckeds(data: T): T;
    }
   
    //Interface status api
    interface IStatusResourceClass<T> extends ng.resource.IResourceClass<ng.resource.IResource<T>> { }

    interface IAnalysisVehicleResourceClass<T> extends ng.resource.IResourceClass<ng.resource.IResource<T>> {
        AnalysisVehicle(data: T): T;
    }
    
    //Topics service api
    export class FormsService {

        private formSvc: IFormResourceClass<any>;
        private checkedSvc: ICheckedResourceClass<any>;
        private readyStatusSvc: IStatusResourceClass<any>;
        private analysisVehicleSvc: IAnalysisVehicleResourceClass<any>;

        static $inject = ['appConfig', '$resource', 'app.shared.VehicleService'];
        constructor(appConfig: app.config.IAppConfig, private $resource: angular.resource.IResourceService, private vehicle: app.shared.VehicleService) {

            //Set service to get forms
            this.formSvc = <IFormResourceClass<any>>$resource(appConfig.FormsUrl, { 'id': '@id' });
            
            //Set service to put checkeds
            this.checkedSvc = <ICheckedResourceClass<any>>$resource(appConfig.CheckedUrl, { 'id': '@VehicleId' }, {
                UpdateCheckeds: { method: 'PUT' }
            });
            
            //Set service to get ready status
            this.readyStatusSvc = <IStatusResourceClass<any>>$resource(appConfig.ReadyStatusUrl, { 'id': '@id' });

            //Set service to analysis vehicle
            this.analysisVehicleSvc = <IAnalysisVehicleResourceClass<any>>$resource(appConfig.AnalysisVehicleUrl, { 'id': '@id' }, {
                AnalysisVehicle: { method: 'PUT' }
            });
        }

        //Get form datas
        public GetForms(): ng.IPromise<any> {
            var formId = this.vehicle.VehicleSelected.VehicleTypeId;
            var IsFormIdMatch = ((formId == 11) || (formId == 13));
            if (!IsFormIdMatch) {
                //Hack: mock form id
                formId = 11;
            }
            return this.formSvc.query({ id: formId }).$promise;
        }
        
        //Get checkeds datas
        public GetCheckeds(): ng.IPromise<CheckedInformation> {
            var vehicleId = this.vehicle.VehicleSelected.id;
            return this.checkedSvc.get({ id: vehicleId }).$promise;
        }

        //Update checkeds datas
        public UpdateCheckeds(checkeds: CheckedInformation): void {
            this.checkedSvc.UpdateCheckeds(checkeds);
        }

        //Get ready status data
        public GetReadyStatus(): ng.IPromise<any> {
            var vehicleId = this.vehicle.VehicleSelected.id
            return this.readyStatusSvc.get({ id: vehicleId }).$promise;
        }

        //Analysis vehicle
        public AnalysisVehicle(vehicle: VehicleInformation): void {
            this.analysisVehicleSvc.AnalysisVehicle(vehicle);
        }
    }

    angular
        .module('app.checking')
        .service('app.checking.FormsService', FormsService);
}
