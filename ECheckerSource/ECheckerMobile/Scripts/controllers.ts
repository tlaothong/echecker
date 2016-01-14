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

        private modal: any;
        private loginData = {};

        static $inject = ["$scope", "$ionicModal", "$timeout"];
        constructor($scope, $ionicModal, private $timeout) {
            $ionicModal.fromTemplateUrl('templates/login.html', {
                backdropClickToClose: false,
                scope: $scope
            }).then((modal) => {
                this.modal = modal;
                $scope.modal = modal;
                $scope.msg = "Hello, from menu!";
            });
        }

        public login() {
            this.modal.show();
        }

        public closeLogin() {
            this.modal.hide();
        }

        public doLogin() {
            console.log('Doing login', this.loginData);

            // Simulate a login delay. Remove this and replace with your login
            // code if using a login system
            this.$timeout(() => {
                this.closeLogin();
            }, 1000);
        }

    }

    angular
        .module('starter.controllers',[])
        .controller('AppCtrl', AppCtrl);
        //.controller('LoginController', LoginController);
}