﻿module app.config {
    'use strict';

    //Create path name each url
    export interface IAppConfig {
        VehiclesUrl: string;
        AddVehicleUrl: string;
        VehicleUrl: string;
        FormsUrl: string;
        CheckedUrl: string;
        NotificationUrl: string;
        ReadyStatusUrl: string;
        AmissedUrl: string;
        ReportUrl: string;
        AnalysisVehicleUrl: string;
        UploadPhoto: string;
    }

    export class AppConfig implements IAppConfig {

        //Set path
        public VehiclesUrl: string;
        public AddVehicleUrl: string;
        public VehicleUrl: string;
        public FormsUrl: string;
        public CheckedUrl: string;
        public NotificationUrl: string;
        public ReadyStatusUrl: string;
        public AmissedUrl: string;
        public ReportUrl: string;
        public AnalysisVehicleUrl: string;
        public UploadPhoto: string;
        
        //Create full path each url
        constructor() {
            var defaulturl = 'http://dltchecker.azurewebsites.net';
            var apiurl = defaulturl + '/api';

            this.VehiclesUrl = apiurl + '/vehicles/:id';
            this.AddVehicleUrl = apiurl + '/vehicle/add';
            this.VehicleUrl = apiurl + '/vehicle/:id';
            this.FormsUrl = apiurl + '/form/:id';
            this.CheckedUrl = apiurl + '/checked/:id';
            this.NotificationUrl = apiurl + '/vehicle/:id/noti';
            this.AmissedUrl = apiurl + '/checked/:id/amissed';
            this.ReportUrl = apiurl + '/Report/:id';
            this.AnalysisVehicleUrl = apiurl + '/checked/:id/done';
            this.UploadPhoto = apiurl + '/checked/:id/:topicid/photo';
            this.ReadyStatusUrl = apiurl + '/checked/:id/readystatus';
        }

    }

    angular
        .module('app', [])
        .service('appConfig', AppConfig);
} 
