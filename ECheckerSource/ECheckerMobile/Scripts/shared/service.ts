module app.shared {
    'use strict';

    export class VehicleService {

        public VehiclesData: any;
    }

    angular
        .module('app.shared')
        .service('app.shared.VehicleService', VehicleService);
}