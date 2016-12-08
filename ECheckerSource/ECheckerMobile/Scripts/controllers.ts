declare var Ionic;
declare var WindowsAzure;

module starter.controllers {

    export class AppCtrl {

        private modalLogin: any;
        private user: any;
        private appUrl = 'https://samplefordevelop.azurewebsites.net';

        static $inject = [
            '$scope',
            '$ionicModal',
            '$state'];
        constructor(
            private $scope,
            private $ionicModal,
            private $state) {

            if (this.user == null) this.user = Ionic.User.current();

            //Prepare login modal
            $ionicModal.fromTemplateUrl('templates/login.html', {
                backdropClickToClose: false,
                hardwareBackButtonClose: false,
                scope: $scope
            }).then((modal) => {
                this.modalLogin = modal;
                if (!this.user.id) this.modalLogin.show();
            });
        }

        //Do login (providerName: Require provider name such facebook or google)
        private login(providerName: string) {

            ////Connect to azure
            var mobileAppsClient = new WindowsAzure.MobileServiceClient(this.appUrl, null);
            if (mobileAppsClient != null) {
                console.log('Connection to azure successed!');

                //Call provider for login
                mobileAppsClient.login(providerName, null)
                    .then((successed) => {
                        var urlRequest = this.appUrl + '/.auth/me';
                        mobileAppsClient._request(
                            'GET',
                            urlRequest,
                            { authenticationToken: successed.mobileServiceAuthenticationToken },
                            true,
                            (error, response) => {
                                if (response != null) {

                                    //Get email from provider account
                                    var emailIndex = 0;
                                    this.user.id = JSON.parse(response.responseText)[emailIndex].user_id;
                                    this.user.save().then(() => {
                                        console.log('login successed with ' + this.user.id + ' account');

                                        //Navigate to index
                                        this.modalLogin.hide();
                                        this.$state.go('app.vehicles', {}, { reload: true });
                                    });
                                }
                            });
                    }, (error) => {
                        //Alert error meesage when failed to login
                        alert('Failed to login with ' + providerName + '\n > ' + error);
                    });
            }
        }

        //Do logout
        private logout() {

            //Remove user account on this mobile
            this.user.delete().then(() => {

                //Reset user account on this mobile
                this.user = Ionic.User.current(new Ionic.User());
                console.log('Log out succeeded.');

                //Logout from azure
                var mobileAppsClient = new WindowsAzure.MobileServiceClient(this.appUrl, null);
                if (mobileAppsClient != null) mobileAppsClient.logout();

                //Display login modal
                this.modalLogin.show()

                //Navigate to index
                this.$state.go('app.vehicles', {}, { reload: true });
            });
        }
    }

    export class Emergency {

        private emergencyPhoneNumber: any = [
            { Title: "แจ้งเหตุด่วนเหตุร้าย", PhoneNumber: "191" },
            { Title: "แจ้งเหตุเพลิงไหม้", PhoneNumber: "199" },
            { Title: "แจ้งเหตุประปาขัดข้อง", PhoneNumber: "1125" },
            { Title: "แจ้งเหตุไฟฟ้าขัดข้อง", PhoneNumber: "1130" },
            { Title: "แจ้งอบุติเหตุ", PhoneNumber: "1137" },
            { Title: "ตำรวจท่องเที่ยว", PhoneNumber: "1155" },
            { Title: "กรมอุตุนิยมวิทยา", PhoneNumber: "1182" },
            { Title: "แจ้งรถหาย", PhoneNumber: "1192" },
            { Title: "ตำรวจทางหลวง", PhoneNumber: "1193" },
            { Title: "กองปราบปราม", PhoneNumber: "1195" },
            { Title: "อุบัติเหตุทางน้ำ", PhoneNumber: "1196" },
            { Title: "ศูนย์ควบคุมจราจร", PhoneNumber: "1197" },
            { Title: "เหตุด่วนทางน้ำ", PhoneNumber: "1199" },
            { Title: "อุบัติเหตุบนทางด่วน", PhoneNumber: "1543" },
            { Title: "แพทย์ฉุกเฉิน", PhoneNumber: "1669" },
            { Title: "สถานีวิทยุร่วมด้วยช่วยกัน", PhoneNumber: "1677" },
            { Title: "กรมการขนส่งทางบก", PhoneNumber: "1584" },
            { Title: "สายด่วน บขส.", PhoneNumber: "1490" },
        ]
    }

    angular
        .module('starter.controllers', [])
        .controller('AppCtrl', AppCtrl)
        .controller('EmergencyCtrl', Emergency);
}