module app.checking {
	'use strict';

    //Interface form api
    interface IFormResourceClass<T> extends ng.resource.IResourceClass<ng.resource.IResource<T>> {
        GetForms(data: T): T;
    }
    
    //Topics service api
    export class FormsService {

        private svc: IFormResourceClass<any>;

        static $inject = ['appConfig', '$resource'];
        constructor(appConfig: app.config.IAppConfig, private $resource: angular.resource.IResourceService) {

            //Set service to get forms
            this.svc = <IFormResourceClass<any>>$resource(appConfig.FormsUrl, { 'id': '@id' }, {
                GetForms: { method: 'Get', isArray: true },
            });
            
        }

        //Get form datas
        public GetForms(): ng.IPromise<FormInformation[]> {
            //Hack: fixed form id
            var formId = 1;
            return this.svc.GetForms(new FormRequest(formId)).$promise;
        }
        
    }

	angular
		.module('app.checking')
        .service('app.checking.FormsService', FormsService);
}