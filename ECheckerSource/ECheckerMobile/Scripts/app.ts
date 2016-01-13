// Ionic Starter App

// angular.module is a global place for creating, registering and retrieving Angular modules
// 'starter' is the name of this angular module example (also set in a <body> attribute in index.html)
// the 2nd parameter is an array of 'requires'
// 'starter.controllers' is found in controllers.js
angular.module('starter', ['ionic', 'starter.controllers', 'app', 'app.shared', 'app.amissed', 'app.checking', 'app.noti', 'app.regis', 'app.vehicles'])

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
        });
    })

    .config(function ($stateProvider, $urlRouterProvider) {
        $stateProvider

            .state('app', {
                url: '/app',
                abstract: true,

                templateUrl: 'templates/sidemenu.html',
                controller: 'AppCtrl'
            })

            .state('app.vehicles', {
                url: '/vehicles',
                views: {
                    'menuContent': {
                        templateUrl: 'templates/vehiclelist.html',
                        controller: 'app.vehicles.VehicleListController as cx',
                        resolve: {
                            "data": ['app.vehicles.VehiclesService', svc => { return svc.GetVehicles() }]
                        }
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
                            "status": ['app.checking.FormsService', svc => { return svc.GetReadyStatus(); }]
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
                url: '/checkamiss/:tid',
                views: {
                    'vContent': {
                        templateUrl: 'templates/checkamiss.html',
                        controller: 'app.checking.TopicsController as cx',
                        resolve: {
                            "data": ["$stateParams", 'app.shared.MockTopics', (p, svc) => {
                                return svc.get(p.tid);
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
                        resolve: {
                            "data": ['app.shared.MockAmissed', svc => { return svc.get('ef1d7bf5-c013-475f-9790-255dc97f8d6f'); }]
                        }
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
                        controller: 'app.amissed.ReportController as cx',
                        resolve: {
                            "data": ['app.shared.MockTopics', svc => { return svc.getAll(); }]
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

            .state('app.editvehicle', {
                url: '/editvehicle',
                views: {
                    'menuContent': {
                        templateUrl: 'templates/updatevehicle.html',
                        controller: 'app.vehicles.VehicleEditController as cx',
                    }
                }
            })


            .state('app.search', {
                url: '/search',
                views: {
                    'menuContent': {
                        templateUrl: 'templates/search.html'
                    }
                }
            })

            .state('app.browse', {
                url: '/browse',
                views: {
                    'menuContent': {
                        templateUrl: 'templates/browse.html'
                    }
                }
            })
            .state('app.playlists', {
                url: '/playlists',
                views: {
                    'menuContent': {
                        templateUrl: 'templates/playlists.html',
                        controller: 'PlaylistsCtrl'
                    }
                }
            })

            .state('app.single', {
                url: '/playlists/:playlistId',
                views: {
                    'menuContent': {
                        templateUrl: 'templates/playlist.html',
                        controller: 'PlaylistCtrl'
                    }
                }
            })

            .state('app.more', {
                url: '/more',
                views: {
                    'menuContent': {
                        templateUrl: 'templates/more.html'
                    }
                }
            })
            .state('app.more.child', {
                url: '/child/:id',
                views: {
                    'tabContent': {
                        templateUrl: 'templates/mchild.html'
                    }
                }
            })
            ;
        // if none of the above states are matched, use this as the fallback
        $urlRouterProvider.otherwise('/app/vehicles');
    });
