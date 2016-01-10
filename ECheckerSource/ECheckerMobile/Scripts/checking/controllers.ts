module app.checking {
    'use strict';
    
    class TopicsController {

        static $inject = ['data', 'app.shared.VehicleService'];
        constructor(private data: any, private vehicle: app.shared.VehicleService) {
        }
        
        //private IsPass(checkTopic: CheckTopicInformation): boolean {
        //    return checkTopic.IsPass == true;
        //}
        //private IsFalse(checkTopic: CheckTopicInformation): boolean {
        //    return checkTopic.IsPass == false;
        //}

    }

    class CheckedController {
        static $inject = ['topics', 'checkeds', 'app.shared.VehicleService'];
        constructor(private topics: TopicInformation[], private checkeds: CheckedInformation, private vehicle: app.shared.VehicleService) {
        }
        
        private IsPass(checkTopic: TopicInformation): boolean {
            var intialIndex = 0;
            var checkTopicInfo = this.checkeds.CheckedTopics.filter(it=> it.TopicId == checkTopic.id)[intialIndex]
            if (checkTopicInfo.IsPass == null) return null;
            return checkTopicInfo.IsPass == true;
        }
        private IsFalse(checkTopic: TopicInformation): boolean {
            var intialIndex = 0;
            var checkTopicInfo = this.checkeds.CheckedTopics.filter(it=> it.TopicId == checkTopic.id)[intialIndex]
            if (checkTopicInfo.IsPass == null) return null;
            return checkTopicInfo.IsPass == false;
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
        .controller('app.checking.CheckedController', CheckedController)
        .controller('app.checking.CheckAmissController', CheckAmissController);
}