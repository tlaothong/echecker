module app.shared {
    'use strict';

    export class VehicleService {

        public VehiclesData = new VehicleInformation();
    }

    export class UserService {
        public UserData = new UserInformation();
        constructor() {
            //Hack: Fixed email
            this.UserData.Email = 'aa@aa.com';
        }
    }

    angular
        .module('app.shared')
        .service('app.shared.VehicleService', VehicleService)
        .service('app.shared.UserService', UserService);
}