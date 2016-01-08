module app.config {
    'use strict';

    //Create path name each url
    export interface IAppConfig {
        VehiclesUrl: string;
        AddVehicleUrl: string;
    }
    
    export class AppConfig implements IAppConfig {

        //Path name for call vehicle api
        public VehiclesUrl: string;
        public AddVehicleUrl: string;
        
        //static $inject = ['defaulturl'];
        //constructor(defaulturl: string) {

        //Create full path each url
        constructor() {
            var defaulturl = 'http://echecker-vanlek.azurewebsites.net';
            var apiurl = defaulturl + '/api';

            this.VehiclesUrl = apiurl + '/vehicles/:id';
            this.AddVehicleUrl = apiurl + '/vehicle/add';
        }
        
    }

    angular
        .module('app', [])
        .service('appConfig', AppConfig);
} 
