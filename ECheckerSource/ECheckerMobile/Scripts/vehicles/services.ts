module app.vehicles {
    'use strict';
    
    //Interface vehicle api
    interface IVehiclesResourceClass<T> extends ng.resource.IResourceClass<ng.resource.IResource<T>> {
        GetVehicles(data: T): T;
    }

    //Interface vehicle api
    interface IAddNewVehiclesResourceClass<T> extends ng.resource.IResourceClass<ng.resource.IResource<T>> {
        AddVehicle(data: T): T;
    }
    
    //Vehicle service api
    export class VehiclesService {

        private getVehiclesvc: IVehiclesResourceClass<any>;
        private addNewVehicleSvc: IAddNewVehiclesResourceClass<any>;

        static $inject = ['appConfig', '$resource', 'app.shared.UserService'];
        constructor(appConfig: app.config.IAppConfig, private $resource: angular.resource.IResourceService, private user: app.shared.UserService) {

            //Set service to get vehicle
            this.getVehiclesvc = <IVehiclesResourceClass<any>>$resource(appConfig.VehiclesUrl, { 'id': '@id' }, {
                GetVehicles: { method: 'Get', isArray: true }
            });

            //Set service to send new vehicle
            this.addNewVehicleSvc = <IAddNewVehiclesResourceClass<any>>$resource(appConfig.AddVehicleUrl, {}, {
                AddVehicle: { method: 'Post' }
            });
        }

        //Get vehicle datas
        public GetVehicles(): ng.IPromise<VehicleInformation[]> {
            var userId = this.user.UserData.Email;
            return this.getVehiclesvc.GetVehicles(new VehiclesRequest(userId)).$promise;
        }

        //Add new vehicle datas
        public AddVehicle(vehicle: VehicleInformation): void {
            this.addNewVehicleSvc.AddVehicle(vehicle);
        }

    }

    angular
        .module('app.vehicles')
        .service('app.vehicles.VehiclesService', VehiclesService);
}