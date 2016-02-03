module app.vehicles {
    'use strict';

    declare var Ionic;
    
    //Interface vehicles api
    interface IVehiclesResourceClass<T> extends ng.resource.IResourceClass<ng.resource.IResource<T>> {}

    //Interface add new vehicle api
    interface IAddNewVehiclesResourceClass<T> extends ng.resource.IResourceClass<ng.resource.IResource<T>> {}

    //Interface vehicle api
    interface IVehicleResourceClass<T> extends ng.resource.IResourceClass<ng.resource.IResource<T>> {
        UpdateVehicle(data: T): T;
    }
    
    //Vehicle service api
    export class VehiclesService {

        private vehiclesSvc: IVehiclesResourceClass<any>;
        private addNewVehicleSvc: IAddNewVehiclesResourceClass<any>;
        private vehicleSvc: IVehicleResourceClass<any>;

        static $inject = ['appConfig', '$resource'];
        constructor(appConfig: app.config.IAppConfig, private $resource: angular.resource.IResourceService) {

            //Set service to get vehicles
            this.vehiclesSvc = <IVehiclesResourceClass<any>>$resource(appConfig.VehiclesUrl, { 'id': '@id' });

            //Set service to send new vehicle
            this.addNewVehicleSvc = <IAddNewVehiclesResourceClass<any>>$resource(appConfig.AddVehicleUrl);

            //Set service to update vehicle
            this.vehicleSvc = <IVehicleResourceClass<any>>$resource(appConfig.VehicleUrl, { 'id': '@id' }, {
                UpdateVehicle: { method: 'Put' }
            });
        }

        //Get vehicles datas
        public GetVehicles(): ng.IPromise<any> {
            var user = Ionic.User.current();
            var userId = user.id;
            return this.vehiclesSvc.query({ id: userId }).$promise;
        }
        
        //Add new vehicle data
        public AddVehicle(vehicle: VehicleInformation): void {
            this.addNewVehicleSvc.save(vehicle);
        }

        //Update vehicle data
        public UpdateVehicle(vehicle: VehicleInformation): void {
            this.vehicleSvc.UpdateVehicle(vehicle);
        }
    }

    angular
        .module('app.vehicles')
        .service('app.vehicles.VehiclesService', VehiclesService);
}