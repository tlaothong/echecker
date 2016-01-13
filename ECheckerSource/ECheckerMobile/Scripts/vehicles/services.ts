module app.vehicles {
    'use strict';
    
    //Interface vehicles api
    interface IVehiclesResourceClass<T> extends ng.resource.IResourceClass<ng.resource.IResource<T>> {
        GetVehicles(data: T): T;
    }

    //Interface add new vehicle api
    interface IAddNewVehiclesResourceClass<T> extends ng.resource.IResourceClass<ng.resource.IResource<T>> {
        AddVehicle(data: T): T;
    }

    //Interface vehicle api
    interface IVehicleResourceClass<T> extends ng.resource.IResourceClass<ng.resource.IResource<T>> {
        UpdateVehicle(data: T): T;
    }
    
    //Vehicle service api
    export class VehiclesService {

        private vehiclesSvc: IVehiclesResourceClass<any>;
        private addNewVehicleSvc: IAddNewVehiclesResourceClass<any>;
        private vehicleSvc: IVehicleResourceClass<any>;

        static $inject = ['appConfig', '$resource', 'app.shared.UserService'];
        constructor(appConfig: app.config.IAppConfig, private $resource: angular.resource.IResourceService, private user: app.shared.UserService) {

            //Set service to get vehicles
            this.vehiclesSvc = <IVehiclesResourceClass<any>>$resource(appConfig.VehiclesUrl, { 'id': '@id' }, {
                GetVehicles: { method: 'Get', isArray: true }
            });

            //Set service to send new vehicle
            this.addNewVehicleSvc = <IAddNewVehiclesResourceClass<any>>$resource(appConfig.AddVehicleUrl, {}, {
                AddVehicle: { method: 'Post' }
            });

            //Set service to update vehicle
            this.vehicleSvc = <IVehicleResourceClass<any>>$resource(appConfig.VehicleUrl, { 'id': '@id' }, {
                UpdateVehicle: { method: 'Put' }
            });
        }

        //Get vehicle datas
        public GetVehicles(): ng.IPromise<VehicleInformation[]> {
            var userId = this.user.UserData.Email;
            return this.vehiclesSvc.GetVehicles(new GetVehiclesRequest(userId)).$promise;
        }
        
        //Add new vehicle datas
        public AddVehicle(vehicle: VehicleInformation): void {
            this.addNewVehicleSvc.AddVehicle(vehicle);
        }

        //Update vehicle datas
        public UpdateVehicle(vehicle: VehicleInformation): void {
            this.vehicleSvc.UpdateVehicle(vehicle);
        }
    }

    angular
        .module('app.vehicles')
        .service('app.vehicles.VehiclesService', VehiclesService);
}