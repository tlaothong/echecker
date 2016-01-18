module app.shared {
    'use strict';
    
    declare var Ionic;

    export class VehicleService {
        public VehicleSelected = new VehicleInformation();
    }
    
    export class FormService {
        public TopicInfos: TopicInformation[];
    }

    export class CheckedsService {
        public CheckedsInfos: CheckedInformation;
    }
    export class AmissDetailService {
        public AmissedInfo = new AmissedInformation();
    }

    angular
        .module('app.shared')
        .service('app.shared.VehicleService', VehicleService)
        .service('app.shared.FormService', FormService)
        .service('app.shared.AmissDetailService', AmissDetailService)
        .service('app.shared.CheckedsService', CheckedsService);
}