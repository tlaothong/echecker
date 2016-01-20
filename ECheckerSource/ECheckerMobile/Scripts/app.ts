// Ionic Starter App

// angular.module is a global place for creating, registering and retrieving Angular modules
// 'starter' is the name of this angular module example (also set in a <body> attribute in index.html)
// the 2nd parameter is an array of 'requires'
// 'starter.controllers' is found in controllers.js
angular.module('starter', ['ionic', 'ionic.service.core', 'ngCordova' , 'starter.controllers', 'app', 'app.shared', 'app.amissed', 'app.checking', 'app.noti', 'app.regis', 'app.report', 'app.vehicles'])

    .run(function ($ionicPlatform) {
        $ionicPlatform.ready(function () {
            // Hide the accessory bar by default (remove this to show the accessory bar above the keyboard
            // for form inputs)
            if (window.cordova && window.cordova.plugins.Keyboard) {
                cordova.plugins.Keyboard.hideKeyboardAccessoryBar(true);
                cordova.plugins.Keyboard.disableScroll(true);

            }
            if (window.StatusBar) {
                // org.apache.cordova.statusbar required
                window.StatusBar.styleDefault();
            }
            Ionic.io();
        });
    })

    .config(function ($stateProvider, $urlRouterProvider) {
        $stateProvider

            .state('app', {
                url: '/app',
                abstract: true,
                templateUrl: 'templates/sidemenu.html',
                controller: 'AppCtrl as cx',
            })

            .state('app.vehicles', {
                url: '/vehicles',
                views: {
                    'menuContent': {
                        templateUrl: 'templates/vehiclelist.html',
                        controller: 'app.vehicles.VehicleListController as cx',
                        //resolve: {
                        //    "data": ['app.vehicles.VehiclesService', svc => { return svc.GetVehicles() }]
                        //}
                    }
                }
            })

            .state('app.vehicle', {
                url: '/vehicle/:vid',
                abstract: true,
                views: {
                    'menuContent': {
                        templateUrl: 'templates/vehicle.html'
                    }
                }
            })

            .state('app.vehicle.status', {
                url: '/status',
                views: {
                    'vContent': {
                        templateUrl: 'templates/vehiclestatus.html',
                        controller: 'app.checking.TopicsController as cx',
                        resolve: {
                            "topics": ['app.checking.FormsService', svc => { return svc.GetForms(); }],
                            "status": ['app.checking.FormsService', svc => { return svc.GetReadyStatus(); }],
                            "amisseds": ['app.amissed.AmissedService', svc => { return svc.GetAmisseds(); }]
                        }
                    }
                }
            })

            .state('app.vehicle.checklists', {
                url: '/checkvehicle',
                views: {
                    'vContent': {
                        templateUrl: 'templates/checkvehicle.html',
                        controller: 'app.checking.CheckedController as cx',
                        resolve: {
                            "data": ['app.checking.FormsService', svc => { return svc.GetCheckeds(); }]
                        }
                    }
                }
            })

            .state('app.vehicle.checkamiss', {
                url: '/checkamiss/:id',
                views: {
                    'vContent': {
                        templateUrl: 'templates/checkamiss.html',
                        controller: 'app.checking.CheckAmissController as cx',
                        resolve: {
                            "data": ["$stateParams", 'app.shared.CheckedsService', (p, svc) => {
                                var intialIndex = 0;
                                return svc.CheckedsInfos.CheckedTopics.filter(it=> it.id == p.id)[intialIndex];
                                
                            }]
                        }
                    }
                }
            })
            .state('app.vehicle.amisseddetail', {
                url: '/amissed',
                views: {
                    'vContent': {
                        templateUrl: 'templates/amisseddetail.html',
                        controller: 'app.amissed.AmissedDetailController as cx',
                       
                    }
                }
            })
            .state('app.vehicle.schedules', {
                url: '/schedules',
                views: {
                    'vContent': {
                        templateUrl: 'templates/notification.html',
                        controller: 'app.noti.NotificationController as cx'
                    }
                }
            })
            .state('app.vehicle.report', {
                url: '/report',
                views: {
                    'vContent': {
                        templateUrl: 'templates/vehiclereport.html',
                        controller: 'app.report.ReportController as cx',
                        resolve: {
                            "data": ['app.report.ReportService', svc => { return svc.GetReport(); }]
                        }
                    }
                }
            })

            .state('app.manvehicles', {
                url: '/manvehicles',
                views: {
                    'menuContent': {
                        templateUrl: 'templates/managevehicle.html',
                        controller: 'app.vehicles.ManageVehicleController as cx',
                        resolve: {
                            "data": ['app.vehicles.VehiclesService', svc => { return svc.GetVehicles() }]
                        }
                    }
                }
            })

            .state('app.addvehicle', {
                url: '/addvehicle',
                views: {
                    'menuContent': {
                        templateUrl: 'templates/addvehicle.html',
                        controller: 'app.vehicles.VehicleAddController as cx'
                    }
                }
            })
            .state('app.vehicle.howto', {
                url: '/howto/:url',
                views: {
                    'vContent': {
                        templateUrl: 'templates/howto.html',
                        controller: 'app.checking.HowToController as cx',
                        resolve: {
                            "url": ['$stateParams', (p) => {
                                return p.url;
                            }]
                        }
                        //templateUrl: 'https://examdeploy.blob.core.windows.net/echecker/browse2.html',
                    }
                }
            })
            .state('app.single', {
                url: '/playlist',
                views: {
                    'menuContent': {
                        templateUrl: 'templates/playlist.html',
                    }
                }
            })
            .state('app.editvehicle', {
                url: '/editvehicle',
                views: {
                    'menuContent': {
                        templateUrl: 'templates/updatevehicle.html',
                        controller: 'app.vehicles.VehicleEditController as cx',
                    }
                }
            });
        // if none of the above states are matched, use this as the fallback
        $urlRouterProvider.otherwise('/app/vehicles');
    });
