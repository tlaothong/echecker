module app.vehicles {
    'use strict';
    
    //Interface vehicle api
    interface IVehiclesResourceClass<T> extends ng.resource.IResourceClass<ng.resource.IResource<T>> {
        GetVehicles(data: T): T;
    }
    
    //Vehicle service api
    export class VehiclesService {

        private svc: IVehiclesResourceClass<any>;

        static $inject = ['appConfig', '$resource'];
        constructor(appConfig: app.config.IAppConfig, private $resource: angular.resource.IResourceService) {

            //Create service for call vehicle api
            this.svc = <IVehiclesResourceClass<any>>$resource(appConfig.VehiclesUrl, { 'id': '@id' }, {
                GetVehicles: { method: 'Get', isArray: true }
            });
        }

        //Get vehicle datas from api
        public GetVehicles(userId: string): ng.IPromise<VehicleInformation[]> {
            return this.svc.GetVehicles(new VehiclesRequest(userId)).$promise;
        }

    }

    angular
        .module('app.vehicles')
        .service('app.vehicles.VehiclesService', VehiclesService);
}