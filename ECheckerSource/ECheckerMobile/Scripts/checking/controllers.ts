module app.checking {
    'use strict';
    
    class TopicsController {

        static $inject = ['data', 'app.shared.VehicleService'];
        constructor(private data: any, private vehicle: app.shared.VehicleService) {
        }

        private IsPass(topic: any): boolean {
            return topic.IsPass == "TRUE";
        }
        private IsFalse(topic: any): boolean {

            //Hack: get true for ispass is null
            if (topic.IsPass == null) {
                return true;
            }

            return topic.IsPass == "FALSE";
        }

    }

    class CheckAmissController {
        static $inject = ['data'];
        constructor(private data) {
        }

    }

    angular
        .module('app.checking')
        .controller('app.checking.TopicsController', TopicsController)
        .controller('app.checking.CheckAmissController', CheckAmissController);
}