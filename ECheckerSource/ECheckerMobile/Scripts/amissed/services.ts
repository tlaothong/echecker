module app.amissed {
    'use strict';

    //Interface amissedService api
    interface IAmissedResourceClass<T> extends ng.resource.IResourceClass<ng.resource.IResource<T>> { }
    
    //AmissedService service api
    export class AmissedService {

        private svc: IAmissedResourceClass<any>;

        static $inject = ['appConfig', '$resource', 'app.shared.VehicleService'];
        constructor(appConfig: app.config.IAppConfig, private $resource: angular.resource.IResourceService, private vehicle: app.shared.VehicleService) {

            //Set service to get vehicles
            this.svc = <IAmissedResourceClass<any>>$resource(appConfig.AmissedUrl, { 'id': '@id' });
        }

        //Get amissed datas
        public GetAmisseds(): ng.IPromise<any> {
            var vehicleId = this.vehicle.VehicleSelected.id;
            return this.svc.query({ id: vehicleId }).$promise;
        }
    }

    angular
        .module('app.amissed')
        .service('app.amissed.AmissedService', AmissedService);
}