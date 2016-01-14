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
    
    //Interface status api
    interface IReportResourceClass<T> extends ng.resource.IResourceClass<ng.resource.IResource<T>> { }

    //Topics service api
    export class FormsService {

        private formSvc: IFormResourceClass<any>;
        private checkedSvc: ICheckedResourceClass<any>;
        private readyStatusSvc: IStatusResourceClass<any>;
        private reportSvc: IReportResourceClass<any>;

        static $inject = ['appConfig', '$resource', 'app.shared.VehicleService'];
        constructor(appConfig: app.config.IAppConfig, private $resource: angular.resource.IResourceService, private vehicle: app.shared.VehicleService) {

            //Set service to get forms
            this.formSvc = <IFormResourceClass<any>>$resource(appConfig.FormsUrl, { 'id': '@id' });
            
            //Set service to get checkeds
            this.checkedSvc = <ICheckedResourceClass<any>>$resource(appConfig.CheckedUrl, { 'id': '@VehicleId' }, {
                UpdateCheckeds: { method: 'PUT' }
            });
            
            //Set service to get ready status
            this.readyStatusSvc = <IStatusResourceClass<any>>$resource(appConfig.ReadyStatusUrl, { 'id': '@id' });

            this.reportSvc = <IStatusResourceClass<any>>$resource(appConfig.ReportUrl, { 'id': '@id' });
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

        //Get report datas
        public GetReport(): ng.IPromise<any> {
            //Hack: fix vehicle id
            var vehicleId = '69C90FD9-5F74-405B-BC24-5C54D3C14252';
            //var vehicleId = this.vehicle.VehicleSelected.id;
            return this.reportSvc.query({ id: vehicleId }).$promise;
        }
    }

    angular
        .module('app.checking')
        .service('app.checking.FormsService', FormsService);
}
