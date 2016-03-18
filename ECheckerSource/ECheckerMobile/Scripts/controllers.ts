//angular.module('starter.controllers', [])

//.controller('AppCtrl', function($scope, $ionicModal, $timeout) {

//  // With the new view caching in Ionic, Controllers are only called
//  // when they are recreated or on app start, instead of every page change.
//  // To listen for when this page is active (for example, to refresh data),
//  // listen for the $ionicView.enter event:
//  //$scope.$on('$ionicView.enter', function(e) {
//  //});

//  // Form data for the login modal
//  $scope.loginData = {};

//  // Create the login modal that we will use later
//  $ionicModal.fromTemplateUrl('templates/login.html', {
//    scope: $scope
//  }).then(function(modal) {
//    $scope.modal = modal;
//  });

//  // Triggered in the login modal to close it
//  $scope.closeLogin = function() {
//    $scope.modal.hide();
//  };

//  // Open the login modal
//  $scope.login = function() {
//    $scope.modal.show();
//  };

//  // Perform the login action when the user submits the login form
//  $scope.doLogin = function() {
//    console.log('Doing login', $scope.loginData);

//    // Simulate a login delay. Remove this and replace with your login
//    // code if using a login system
//    $timeout(function() {
//      $scope.closeLogin();
//    }, 1000);
//  };
//})

//.controller('PlaylistsCtrl', function($scope) {
//  $scope.playlists = [
//    { title: 'Reggae', id: 1 },
//    { title: 'Chill', id: 2 },
//    { title: 'Dubstep', id: 3 },
//    { title: 'Indie', id: 4 },
//    { title: 'Rap', id: 5 },
//    { title: 'Cowbell', id: 6 }
//  ];
//})

//.controller('PlaylistCtrl', function($scope, $stateParams) {
//});

declare var Ionic;
declare var WindowsAzure;

module starter.controllers {

    export class AppCtrl {

        private modalLogin: any;
        private modalRegister: any;
        private user: any;
        private email: any;
        private password: any;
        private appUrl = 'https://samplefordevelop.azurewebsites.net';

        static $inject = [
            "$scope",
            "$ionicModal",
            "$timeout",
            '$state'];
        constructor(
            private $scope,
            private $ionicModal,
            private $timeout,
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

            //Prepare register modal
            $ionicModal.fromTemplateUrl('templates/register.html', {
                backdropClickToClose: false,
                hardwareBackButtonClose: false,
                scope: $scope
            }).then((modal) => {
                this.modalRegister = modal;
            });

        }
        
        //Call register modal
        private callRegister() {
            this.clear();
            this.modalRegister.show();
        }

        //Do register
        private register() {
            this.user.id = this.email;
            console.log('Register succeeded.');
            this.user.save().then(() => { this.navigateToIndex(); });
        }

        //Cancel register
        private cancelRegister() {
            this.clear();
            this.modalRegister.hide();
        }
        
        //Do login
        private login() {
            if (this.checkLogin(this.email, this.password)) {
                console.log('Login succeeded.');
                this.user.id = this.email;
                this.user.save().then(() => { this.navigateToIndex(); });
            } else alert('Email or password is not correct.');
        }

        //Do logout
        private logout() {
            //alert('Before save: ' + this.user.isValid());
            this.user.delete().then(() => {
                this.user = Ionic.User.current(new Ionic.User());
                console.log('Log out succeeded.');
                this.modalLogin.show()
                this.$state.go('app.vehicles', {}, { reload: true });
            });
        }

        //Check login
        private checkLogin(email: string, password: string): boolean {
            //Hack: mock email and password for checking correct on login
            var isAuthenticated = ((email == 'aa@aa.com') && (password == 'password'));
            return isAuthenticated;
        }
        
        //Do login with facebook account
        private facebookLogin() {
            var mobileAppsClient = new WindowsAzure.MobileServiceClient(this.appUrl, null);
            if (mobileAppsClient != null) {
                console.log('Connection to azure successed!');
                mobileAppsClient.login('facebook', null)
                    .then((successed) => {

                        var urlRequest = this.appUrl + '/.auth/me';
                        mobileAppsClient._request(
                            'GET',
                            urlRequest,
                            { authenticationToken: successed.mobileServiceAuthenticationToken },
                            true,
                            (error, response) => {
                                var emailIndex = 0;
                                this.user.id = JSON.parse(response.responseText)[emailIndex].user_id;
                                this.user.save().then(() => { this.navigateToIndex(); });
                            });

                    }, function (error) {
                        alert('Failed to login with facebook\n > ' + error);
                    });
            }
        }

        //Do login with google account
        private googleLogin() {
            var mobileAppsClient = new WindowsAzure.MobileServiceClient(this.appUrl, null);
            if (mobileAppsClient != null) {
                console.log('Connection to azure successed!');
                mobileAppsClient.login('google', null)
                    .then((successed) => {

                        var urlRequest = this.appUrl + '/.auth/me';
                        mobileAppsClient._request(
                            'GET',
                            urlRequest,
                            { authenticationToken: successed.mobileServiceAuthenticationToken },
                            true,
                            (error, response) => {
                                var emailIndex = 0;
                                this.user.id = JSON.parse(response.responseText)[emailIndex].user_id;
                                this.user.save().then(() => { this.navigateToIndex(); });
                            });

                    }, function (error) {
                        alert('Failed to login with google\n > ' + error);
                    });
            }
        }

        //Clear email and password input
        private clear() {
            this.email = '';
            this.password = '';
        }

        //Navigate to index and clean eveything
        private navigateToIndex() {
            this.clear();
            this.modalRegister.hide();
            this.modalLogin.hide();
            this.$state.go('app.vehicles', {}, { reload: true });
        }
        
        //public login() {
        //    this.modal.show();
        //}

        //public closeLogin() {
        //    this.modal.hide();
        //}

        //public doLogin() {
        //    console.log('Doing login', this.loginData);

        //    // Simulate a login delay. Remove this and replace with your login
        //    // code if using a login system
        //    this.$timeout(() => {
        //        this.closeLogin();
        //    }, 1000);
        //}
        
        //Login successed
        //public Logined() {
        //    //Delay 1 sec to hide modal
            
        //    //TODO: user ionic get email
        //    //this.user.IsLogin = true;
        //    console.log('Login succeed.');
        //    this.$timeout(() => { this.modalLogin.hide(); }, 1000);
        //}
    }

    angular
        .module('starter.controllers', [])
        .controller('AppCtrl', AppCtrl);
}