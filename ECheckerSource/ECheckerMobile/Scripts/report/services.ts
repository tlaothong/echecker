module app.report {
    'use strict';
    
    //Interface status api
    interface IReportResourceClass<T> extends ng.resource.IResourceClass<ng.resource.IResource<T>> { }

    //Topics service api
    export class ReportService {
    
        private svc: IReportResourceClass<any>;

        static $inject = ['appConfig', '$resource', 'app.shared.VehicleService'];
        constructor(appConfig: app.config.IAppConfig, private $resource: angular.resource.IResourceService, private vehicle: app.shared.VehicleService) {

            this.svc = <IReportResourceClass<any>>$resource(appConfig.ReportUrl, { 'id': '@id' });
        }

        //Get report datas
        public GetReport(): ng.IPromise<any> {
            var vehicleId = this.vehicle.VehicleSelected.id;
            return this.svc.query({ id: vehicleId }).$promise;
        }
    }

    angular
        .module('app.report')
        .service('app.report.ReportService', ReportService);
}
