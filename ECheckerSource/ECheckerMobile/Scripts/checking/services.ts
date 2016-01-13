module app.checking {
    'use strict';

    //Interface form api
    interface IFormResourceClass<T> extends ng.resource.IResourceClass<ng.resource.IResource<T>> {
        GetForms(data: T): T;
    }

    interface ICheckedResourceClass<T> extends ng.resource.IResourceClass<ng.resource.IResource<T>> {
        GetCheckeds(data: T): T;
    }
   
    //Interface vehicle api
    interface IStatusResourceClass<T> extends ng.resource.IResourceClass<ng.resource.IResource<T>> {
        GetReadyStatus(data: T): T;
    }

    //Topics service api
    export class FormsService {

        private formSvc: IFormResourceClass<any>;
        private checkedSvc: ICheckedResourceClass<any>;
        private readyStatusSvc: IStatusResourceClass<any>;

        static $inject = ['appConfig', '$resource', 'app.shared.VehicleService'];
        constructor(appConfig: app.config.IAppConfig, private $resource: angular.resource.IResourceService, private vehicle: app.shared.VehicleService) {

            //Set service to get forms
            this.formSvc = <IFormResourceClass<any>>$resource(appConfig.FormsUrl, { 'id': '@id' }, {
                GetForms: { method: 'Get', isArray: true },
            });
            
            //Set service to get checkes
            this.checkedSvc = <ICheckedResourceClass<any>>$resource(appConfig.CheckedUrl, { 'id': '@id' }, {
                GetCheckeds: { method: 'Get' },
            });
            
            //Set service to get ready status
            this.readyStatusSvc = <IStatusResourceClass<any>>$resource(appConfig.ReadyStatusUrl, { 'id': '@id' }, {
                GetReadyStatus: { method: 'Get' }
            });
        }

        //Get form datas
        public GetForms(): ng.IPromise<TopicInformation[]> {
            var formId = this.vehicle.VehicleSelected.VehicleTypeId;
            var IsFormIdMatch = ((formId == 11) || (formId == 13)) ? true : false;
            if (!IsFormIdMatch) {
                //Hack: mock form id
                formId = 11;
            }
            return this.formSvc.GetForms(new FormRequest(formId)).$promise;
        }
        
        //Get checked datas
        public GetCheckeds(): ng.IPromise<CheckedInformation> {
            //Hack: fixed vehicle id
            var vehicleId = '69C90FD9-5F74-405B-BC24-5C54D3C14252';
            //var vehicleId = this.vehicle.VehicleSelected.id;
            return this.checkedSvc.GetCheckeds(new CheckedRequest(vehicleId)).$promise;
        }

        public GetReadyStatus(): ng.IPromise<Object> {
            var vehicleId = this.vehicle.VehicleSelected.id
            return this.readyStatusSvc.GetReadyStatus(new GetReadyStatusRequest(vehicleId)).$promise;
        }
    }

    angular
        .module('app.checking')
        .service('app.checking.FormsService', FormsService);
}
