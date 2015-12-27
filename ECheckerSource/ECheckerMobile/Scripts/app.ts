// Ionic Starter App

// angular.module is a global place for creating, registering and retrieving Angular modules
// 'starter' is the name of this angular module example (also set in a <body> attribute in index.html)
// the 2nd parameter is an array of 'requires'
// 'starter.controllers' is found in controllers.js
angular.module('starter', ['ionic', 'starter.controllers', 'app.shared', 'app.amissed', 'app.checking', 'app.noti', 'app.regis', 'app.vehicles'])

.run(function($ionicPlatform) {
  $ionicPlatform.ready(function() {
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

.config(function($stateProvider, $urlRouterProvider) {
    $stateProvider

        .state('app', {
            url: '/app',
            abstract: true,
            
            templateUrl: 'templates/sidemenu.html',
            controller: 'AppCtrl'
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
                    templateUrl: 'templates/vehicle-status.html'
                }
            }
        })
        
        .state('app.vehicles', {
            url: '/vehicles',
            views: {
                'menuContent': {
                    templateUrl: 'templates/VehicleList.html',
                    controller: 'app.vehicles.VehicleListController as cx',
                    resolve: {
                        "data": ['app.shared.MockVehicles', svc => { return svc.getAll(); }]
                    }
                }
            }
        })
        .state('app.manvehicles', {
            url: '/manvehicles',
            views: {
                'menuContent': {
                    templateUrl: 'templates/ManageVehicle.html',
                    controller: 'app.vehicles.ManageVehicleController as cx',
                    resolve: {
                        "data": ['app.shared.MockVehicles', svc => { return svc.getAll(); }]
                    }
                }
            }
        })
        .state('app.editvehicle', {
            url: '/editvehicle/:vid',
            views: {
                'menuContent': {
                    templateUrl: 'templates/UpdateVehicle.html',
                    controller: 'app.vehicles.VehicleEditController as cx',
                    resolve: {
                        "data": ["$stateParams", 'app.shared.MockVehicles', (p, svc) => {
                            return svc.get(p.vid);
                        }]
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
        .state('app.schedules', {
            url: '/schedules/',
            views: {
                'menuContent': {
                    templateUrl: 'templates/notification.html',
                    controller: 'app.noti.NotificationController as cx'
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
