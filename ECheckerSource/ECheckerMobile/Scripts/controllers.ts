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

module starter.controllers {

    //export class LoginController {

    //    static $inject = ["$scope"];
    //    constructor(private $scope) {
    //    }

    //    public doLogin() {
    //        alert('hi');
    //        this.$scope.modal.hide();
    //    }
    //}

    export class AppCtrl {

        private modalLogin: any;
        private modalRegister: any;
        //private loginData = {};
        private user: any;
        private email: any;
        private password: any;

        static $inject = ["$scope", "$ionicModal", "$timeout", '$state'];
        constructor($scope, $ionicModal, private $timeout, private $state) {

            Ionic.io();
            this.user = Ionic.User.current();

            //Prepare login modal
            $ionicModal.fromTemplateUrl('templates/login.html', {
                backdropClickToClose: false,
                scope: $scope
            }).then((modal) => {
                this.modalLogin = modal;
                if (!this.user.id) this.modalLogin.show();
            });

            //Prepare register modal
            $ionicModal.fromTemplateUrl('templates/register.html', {
                backdropClickToClose: false,
                scope: $scope
            }).then((modal) => {
                this.modalRegister = modal;
            });
        }
        
        //Call register modal
        private register() {
            this.clear();
            this.modalRegister.show();
        }

        //Do register
        private submitToRegister() {
            this.user.id = this.email;
            this.modalRegister.hide();
            this.modalLogin.hide();
            this.$state.go('app.vehicles', {}, { reload: true });
        }

        //Cancel to register
        private cancelToRegister() {
            this.clear();
            this.modalRegister.hide();
        }
        
        //Do login
        private login() {
            this.user.id = this.email;
            this.modalLogin.hide();
            this.$state.go('app.vehicles', {}, { reload: true });
        }

        //Logout from system
        private logout() {
            this.user = Ionic.User.current({});
            //this.user = Ionic.User.current();

            //alert('Before save: ' + this.user);
            this.user.save();
            //alert('After save: ' + this.user);
            console.log('Log out succeed.');
            this.$state.go('app.vehicles', {}, { reload: true });
        }

        //Clear email and password input
        private clear() {
            this.email = '';
            this.password = '';
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
    //.controller('LoginController', LoginController);
}