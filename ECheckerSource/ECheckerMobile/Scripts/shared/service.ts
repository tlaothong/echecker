module app.shared {
    'use strict';

    export class VehicleService {
        public VehicleSelected = new VehicleInformation();
    }

    export class UserService {
        public UserData = new UserInformation();
        constructor() {
            //Hack: Fixed email
            this.UserData.Email = 'aa@aa.com';
        }
    }

    export class FormService {
        public TopicInfos: TopicInformation[];
    }

    export class CheckedsService {
        public CheckedsInfos: CheckedInformation;
    }

    angular
        .module('app.shared')
        .service('app.shared.VehicleService', VehicleService)
        .service('app.shared.UserService', UserService)
        .service('app.shared.FormService', FormService)
        .service('app.shared.CheckedsService', CheckedsService);
}