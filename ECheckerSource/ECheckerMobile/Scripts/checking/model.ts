module app.checking {
    'use strict';    

    export class FormInformation {
        id: string;
        VehicleTypeId: number;
        Detail: string;
        IsCritical: boolean;
        DamagePercent: number;
        How2Topic: string;
        How2Url: string;
        SuggestTopic: string;
        SuggestDetail: string;
        CreateDate: Date;
        FormId: number;
    }
    
    export class FormRequest {
        constructor(public id: number) { };
    }

    export class Checkeds {

        public id: number;
        public checkedtopics: CheckedTopic[];

        //public model: any = null;

        //static $inject = ['data', '$scope', 'app.shared.SampleDataService'];
        //constructor(public data, private $scope: ng.IScope, public SampleDataService: IMockDataService) {
        //}

        //// public myMethod(): void {
        //// }

    }

    export class CheckedTopic
    {
        public id: number;
        public ispass: boolean;
    }
}