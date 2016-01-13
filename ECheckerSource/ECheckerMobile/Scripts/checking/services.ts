module app.checking {
    'use strict';

    //Interface form api
    interface IFormResourceClass<T> extends ng.resource.IResourceClass<ng.resource.IResource<T>> {}

    interface ICheckedResourceClass<T> extends ng.resource.IResourceClass<ng.resource.IResource<T>> {
        UpdateCheckeds(data: T): T;
    }
   
    //Interface vehicle api
    interface IStatusResourceClass<T> extends ng.resource.IResourceClass<ng.resource.IResource<T>> {}

    //Topics service api
    export class FormsService {

        private formSvc: IFormResourceClass<any>;
        private checkedSvc: ICheckedResourceClass<any>;
        private readyStatusSvc: IStatusResourceClass<any>;

        static $inject = ['appConfig', '$resource', 'app.shared.VehicleService'];
        constructor(appConfig: app.config.IAppConfig, private $resource: angular.resource.IResourceService, private vehicle: app.shared.VehicleService) {

            //Set service to get forms
            this.formSvc = <IFormResourceClass<any>>$resource(appConfig.FormsUrl, { 'id': '@id' });
            
            //Set service to get checkes
            this.checkedSvc = <ICheckedResourceClass<any>>$resource(appConfig.CheckedUrl, { 'id': '@VehicleId' }, {
                UpdateCheckeds: { method: 'PUT' }
            });
            
            //Set service to get ready status
            this.readyStatusSvc = <IStatusResourceClass<any>>$resource(appConfig.ReadyStatusUrl, { 'id': '@id' });
        }

        //Get form datas
        public GetForms(): ng.IPromise<any> {
            var formId = this.vehicle.VehicleSelected.VehicleTypeId;
            var IsFormIdMatch = ((formId == 11) || (formId == 13)) ? true : false;
            if (!IsFormIdMatch) {
                //Hack: mock form id
                formId = 11;
            }
            return this.formSvc.query({ id: formId }).$promise;
        }
        
        //Get checked datas
        public GetCheckeds(): ng.IPromise<CheckedInformation> {
            var vehicleId = this.vehicle.VehicleSelected.id;
            return this.checkedSvc.get({ id: vehicleId }).$promise;
        }

        public UpdateCheckeds(checkeds: CheckedInformation): void {
            this.checkedSvc.UpdateCheckeds(checkeds);
        }

        public GetReadyStatus(): ng.IPromise<Object> {
            var vehicleId = this.vehicle.VehicleSelected.id
            return this.readyStatusSvc.get({ id: vehicleId }).$promise;
        }
    }

    angular
        .module('app.checking')
        .service('app.checking.FormsService', FormsService);
}
