module app.config {
    'use strict';

    //Create path name each url
    export interface IAppConfig {
        VehiclesUrl: string;
        AddVehicleUrl: string;
        VehicleUrl: string;
    }
    
    export class AppConfig implements IAppConfig {

        //Set path
        public VehiclesUrl: string;
        public AddVehicleUrl: string;
        public VehicleUrl: string;
        
        //static $inject = ['defaulturl'];
        //constructor(defaulturl: string) {

        //Create full path each url
        constructor() {
            var defaulturl = 'http://echecker-vanlek.azurewebsites.net';
            var apiurl = defaulturl + '/api';

            this.VehiclesUrl = apiurl + '/vehicles/:id';
            this.AddVehicleUrl = apiurl + '/vehicle/add';
            this.VehicleUrl = apiurl + '/vehicle/:id';

        }
        
    }

    angular
        .module('app', [])
        .service('appConfig', AppConfig);
} 
