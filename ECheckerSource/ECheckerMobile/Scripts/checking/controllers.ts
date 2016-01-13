module app.checking {
    'use strict';

    class TopicsController {

        static $inject = ['topics', 'status', 'app.shared.VehicleService', 'app.shared.FormService'];
        constructor(private topics: any, private status: any, private vehicle: app.shared.VehicleService, private topicsService: app.shared.FormService) {
            topicsService.TopicInfos = topics;
        }
        
        //private IsPass(checkTopic: CheckTopicInformation): boolean {
        //    return checkTopic.IsPass == true;
        //}
        //private IsFalse(checkTopic: CheckTopicInformation): boolean {
        //    return checkTopic.IsPass == false;
        //}

    }

    class CheckedController {
        static $inject = ['data', 'app.shared.VehicleService', 'app.checking.FormsService', 'app.shared.FormService'];
        constructor(private data: CheckedInformation, private vehicle: app.shared.VehicleService, private svc: app.checking.FormsService, private topics: app.shared.FormService) {
        
            //Reload topics when topics is null
            var isTopicsNull = topics.TopicInfos == null ? true : false;
            if (isTopicsNull) {
                console.log('Topics is missing.');
                console.log('Retry download topics again.');
                svc.GetForms().then((it) => {
                    topics.TopicInfos = it;
                    console.log('Donwload topics completed!');
                });
            }
            //Reload checkeds when checkeds is null
            var isCheckedsNull = data == null ? true : false;
            if (isCheckedsNull) {
                console.log('Checkeds is missing.');
                console.log('Retry download checkeds again.');
                svc.GetCheckeds().then((it) => {
                    data = it;
                    console.log('Donwload checkeds completed!');
                });
            }

        }

        private IsPass(checkTopic: TopicInformation): boolean {
            var intialIndex = 0;
            var checkTopicInfo = this.data.CheckedTopics.filter(it=> it.TopicId == checkTopic.id)[intialIndex]
            if (checkTopicInfo.IsPass == null) return null;
            return checkTopicInfo.IsPass == true;
        }
        private IsFalse(checkTopic: TopicInformation): boolean {
            var intialIndex = 0;
            var checkTopicInfo = this.data.CheckedTopics.filter(it=> it.TopicId == checkTopic.id)[intialIndex]
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