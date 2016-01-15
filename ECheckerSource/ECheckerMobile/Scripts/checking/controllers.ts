module app.checking {
    'use strict';

    class TopicsController {

        static $inject = [
            '$state',
            'topics',
            'status',
            'amisseds',
            'app.shared.VehicleService',
            'app.shared.FormService',
            'app.shared.CheckedsService',
            'app.shared.AmissDetailService',
            'app.checking.FormsService'];
        constructor(
            private $state,
            private topics: any,
            private status: any,
            private amisseds: any,
            private vehicle: app.shared.VehicleService,
            private topicsService: app.shared.FormService,
            private checkeds: app.shared.CheckedsService,
            private amissed: app.shared.AmissDetailService,
            private svc: app.checking.FormsService) {
            topicsService.TopicInfos = topics;
        }

        private IsDisableToAnalysis(): boolean {
            var ReadyToAnalysisCode = 1;
            var IsReadyToAnalysis = this.vehicle.VehicleSelected.StatusCode == ReadyToAnalysisCode;
            var IsCheckedsHaveValue = ((this.checkeds.CheckedsInfos != null) && (this.checkeds.CheckedsInfos.CheckedTopics != null));

            return !(
                ((IsCheckedsHaveValue) && (this.checkeds.CheckedsInfos.CheckedTopics.every(it=> it.IsPass != null))) ||
                (IsReadyToAnalysis)
            );
        }

        private Analysis(): void {
            this.svc.AnalysisVehicle(this.vehicle.VehicleSelected);
            this.checkeds.CheckedsInfos = null;
            console.log('Analysis is done, Back to vehilce list.');
            this.$state.go('app.vehicles');
        }

        private SelectAmissedDeatil(amissed: AmissedInformation) {
            this.amissed.AmissedInfo = amissed;
        }

    }

    class CheckedController {
        static $inject = [
            'data',
            'app.shared.VehicleService',
            'app.checking.FormsService',
            'app.shared.FormService',
            'app.shared.CheckedsService'];
        constructor(
            private data: CheckedInformation,
            private vehicle: app.shared.VehicleService,
            private svc: app.checking.FormsService,
            private topics: app.shared.FormService,
            private checkeds: app.shared.CheckedsService) {

            //Reload topics when topics is null
            var isTopicsNull = topics.TopicInfos == null;
            if (isTopicsNull) {
                console.log('Topics is missing.');
                console.log('Retry download topics again.');
                svc.GetForms().then((it) => {
                    topics.TopicInfos = it;
                    console.log('Donwload topics completed!');
                });
            }
            //Reload checkeds when checkeds is null
            var isCheckedsNull = data == null;
            if (isCheckedsNull) {
                console.log('Checkeds is missing.');
                console.log('Retry download checkeds again.');
                svc.GetCheckeds().then((it) => {
                    data = it;
                    console.log('Donwload checkeds completed!');
                });
            }
            checkeds.CheckedsInfos = data;
        }

        private IsPass(checkTopic: TopicInformation): boolean {
            var intialIndex = 0;
            var checkTopicInfo = this.data.CheckedTopics.filter(it=> it.id == checkTopic.id)[intialIndex]
            if (checkTopicInfo.IsPass == null) return null;
            return checkTopicInfo.IsPass == true;
        }
        private IsFalse(checkTopic: TopicInformation): boolean {
            var intialIndex = 0;
            var checkTopicInfo = this.data.CheckedTopics.filter(it=> it.id == checkTopic.id)[intialIndex]
            if (checkTopicInfo.IsPass == null) return null;
            return checkTopicInfo.IsPass == false;
        }
    }

    class CheckAmissController {

        private Detail: string;
        private AmissIsPass: boolean;

        static $inject = [
            '$state',
            '$cordovaCamera',
            'data',
            'app.shared.VehicleService',
            'app.shared.FormService',
            'app.shared.CheckedsService',
            'app.checking.FormsService'
        ];
        constructor(
            private $state: any,
            private $cordovaCamera, 
            private data: CheckTopicInformation,
            private vehicle: app.shared.VehicleService,
            private topics: app.shared.FormService,
            private checkeds: app.shared.CheckedsService,
            private svc: app.checking.FormsService) {

            var intialIndex = 0;
            this.Detail = topics.TopicInfos.filter(it=> it.id == data.id)[intialIndex].Detail;
            this.AmissIsPass = data.IsPass;
        }

        private IsDisableToSubmit(): boolean {
            return this.AmissIsPass == null;
        }

        private Submit(): void {
            this.data.IsPass = this.AmissIsPass;
            var intialIndex = 0;
            this.checkeds.CheckedsInfos.CheckedTopics.filter(it=> it.id == this.data.id)[intialIndex] = this.data;
            this.svc.UpdateCheckeds(this.checkeds.CheckedsInfos);
            this.$state.go('app.vehicle.checklists');
        }

        private Capture(): void {

            var options = {
                quality: 50,
                destinationType: Camera.DestinationType.DATA_URL,
                sourceType: Camera.PictureSourceType.CAMERA,
                allowEdit: true,
                encodingType: Camera.EncodingType.JPEG,
                targetWidth: 100,
                targetHeight: 100,
                saveToPhotoAlbum: false,
                correctOrientation: true
            };

            this.$cordovaCamera.getPicture(options).then(function (imageData) {
                alert('Can capture.');


              
                //var image = <HTMLImageElement>document.getElementById('myImage');
                //image.src = "data:image/jpeg;base64," + imageData;

                //upload to blob


                //get url
                //var photoUrl = .....

                //add urlto service
                //this.data.PhotoURL = photoUrl


            }, function (err) {
                alert('Capture failed.');
                // error
                });

        }
    }

    angular
        .module('app.checking')
        .controller('app.checking.TopicsController', TopicsController)
        .controller('app.checking.CheckedController', CheckedController)
        .controller('app.checking.CheckAmissController', CheckAmissController);
}