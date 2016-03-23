module app.checking {
    'use strict';

    class TopicsController {

        static $inject = [
            '$ionicLoading',
            '$timeout',
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
            private $ionicLoading,
            private $timeout,
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
            
            //Delay 3 seconds before go to vehicle list
            var secondDelay = 3;
            var millisecondDelay = secondDelay * 1000;
            this.$ionicLoading.show({
                template: 'Loading... <ion-spinner></ion-spinner>'
            });

            this.$timeout(() => {
                this.$ionicLoading.hide();
                this.$state.go('app.vehicles');
            }, millisecondDelay);
        }

        private SelectAmissedDeatil(amissed: AmissedInformation) {
            this.amissed.AmissedInfo = amissed;
        }

        //Display percent of checkeds
        private DisplayCheckRatio(): string {
            return this.status.ReadyStatus.split(' ')[0];
        }

        //Display vehicle status
        private DisplayVehicleStatus(): string {
            return this.status.ReadyStatus.split(' ')[1];
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
            var isTopicsNull = this.topics == null || this.topics.TopicInfos == null;
            if (isTopicsNull) {
                console.log('Topics is missing.');
                console.log('Retry download topics again.');
                svc.GetForms().then((it) => {
                    this.topics.TopicInfos = it;
                    console.log('Donwload topics completed!');
                });

            }
            //Reload checkeds when checkeds is null
            if (this.checkeds == null || this.checkeds.CheckedsInfos == null) {
                var isCheckedsNull = data == null;
                if (isCheckedsNull) {
                    console.log('Checkeds is missing.');
                    console.log('Retry download checkeds again.');
                    svc.GetCheckeds().then((it) => {
                        data = it;
                        console.log('Donwload checkeds completed!');
                        this.checkeds.CheckedsInfos = data;
                    });
                } else {
                    checkeds.CheckedsInfos = data;
                }
            } else if (this.checkeds.CheckedsInfos.VehicleId !== this.vehicle.VehicleSelected.id) {
                var isCheckedsNull = data == null;
                if (isCheckedsNull) {
                    console.log('Checkeds is missing.');
                    console.log('Retry download checkeds again.');
                    svc.GetCheckeds().then((it) => {
                        data = it;
                        console.log('Donwload checkeds completed!');
                        this.checkeds.CheckedsInfos = data;
                    });
                } else {
                    this.checkeds.CheckedsInfos = data;
                }
            }
        }

        private IsPass(checkTopic: TopicInformation): boolean {
            var intialIndex = 0;
            var checkTopicInfo = this.checkeds.CheckedsInfos.CheckedTopics.filter(it=> it.id == checkTopic.id)[intialIndex]
            if (checkTopicInfo.IsPass == null) return null;
            return checkTopicInfo.IsPass == true;
        }
        private IsFalse(checkTopic: TopicInformation): boolean {
            var intialIndex = 0;
            var checkTopicInfo = this.checkeds.CheckedsInfos.CheckedTopics.filter(it=> it.id == checkTopic.id)[intialIndex]
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
            '$cordovaFileTransfer',
            'data',
            'app.shared.VehicleService',
            'app.shared.FormService',
            'app.shared.CheckedsService',
            'app.checking.FormsService'
        ];
        constructor(
            private $state: any,
            private $cordovaCamera,
            private $cordovaFileTransfer,
            private data: CheckTopicInformation,
            private vehicle: app.shared.VehicleService,
            private topics: app.shared.FormService,
            private checkeds: app.shared.CheckedsService,
            private svc: app.checking.FormsService,
            private howtourl: string) {

            var intialIndex = 0;
            this.Detail = topics.TopicInfos.filter(it=> it.id == data.id)[intialIndex].Detail;
            this.AmissIsPass = data.IsPass;
            this.howtourl = topics.TopicInfos.filter(it=> it.id == data.id)[intialIndex].How2Url;
        }

        public OpenHowTo(): void {
            window.open(this.howtourl, 'vContent', 'location=yes');
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

            //Set options for camera and file transfer
            var options = {
                destinationType: Camera.DestinationType.FILE_URI,
                sourceType: Camera.PictureSourceType.CAMERA
            };

            //Prepare connection API service
            var intialIndex = 0;
            var topicId = this.checkeds.CheckedsInfos.CheckedTopics.filter(it=> it.id == this.data.id)[intialIndex].id;
            var apiUrl = 'http://echecker-vanlek.azurewebsites.net/api/checked/' + this.vehicle.VehicleSelected.id + '/' + topicId + '/photo';
            
            //Get Images
            this.$cordovaCamera.getPicture(options)
                .then((imageData) => {
                
                    //Upload Images
                    this.$cordovaFileTransfer.upload(apiUrl, imageData, options)
                        .then((result) => {
                            var photourl: string = result.response;
                            var intiIndex: number = 13;
                            var endIndex: number = photourl.length - 2;
                            this.data.PhotoURL = photourl.substring(intiIndex, endIndex);
                        }, function (uploadFailedMessage) {
                            alert('Upload failed.\n' + uploadFailedMessage);
                        }, function (uploadProgress) {
                            console.log('Upload progress: ' + (uploadProgress.loaded / uploadProgress.total) * 100);
                        });
                }, function (captureFailedMessage) {
                    alert('Capture failed.\n' + captureFailedMessage);
                });
        }
    }

    class HowToController {

        private elm: HTMLDivElement;

        static $inject = ['url', '$http'];
        constructor(public url, private $http: ng.IHttpService) {
            this.elm = <HTMLDivElement>document.getElementById("how2content");
            this.$http.get(url).then(h => { this.elm.innerHTML = <string>h.data; });
        }

    }

    angular
        .module('app.checking')
        .controller('app.checking.TopicsController', TopicsController)
        .controller('app.checking.CheckedController', CheckedController)
        .controller('app.checking.CheckAmissController', CheckAmissController)
        .controller('app.checking.HowToController', HowToController);
}