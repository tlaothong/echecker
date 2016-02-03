module app.noti {
    'use strict';


    //Interface vehicles api
    interface INotificationResourceClass<T> extends ng.resource.IResourceClass<ng.resource.IResource<T>> {
        UpdateNotification(data: T): T;
    }

    //Vehicle service api
    export class NotificationService {

        private svc: INotificationResourceClass<any>;

        static $inject = ['appConfig', '$resource'];
        constructor(appConfig: app.config.IAppConfig, private $resource: angular.resource.IResourceService) {

            //Set service to get vehicles
            this.svc = <INotificationResourceClass<any>>$resource(appConfig.NotificationUrl, { 'id': '@id' }, {
                UpdateNotification: { method: 'Put' },
            });
        }

        public UpdateNotification(vehicle: VehicleNotificationInformation): void {
            this.svc.UpdateNotification(vehicle);
        }
    }

    angular
        .module('app.noti')
        .service('app.noti.NotificationService', NotificationService);
}