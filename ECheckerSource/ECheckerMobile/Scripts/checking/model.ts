module app.checking {
    'use strict';    

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