module app.checking {
    'use strict';    
    
    export class FormRequest {
        constructor(public id: number) { };
    }

    export class CheckedInformation {
        id: string;
        CheckedTopics: CheckTopicInformation[];
        IsDone: boolean;
        CreateDate: Date;
        VehicleId: string;
    }

    export class CheckTopicInformation {
        id: string;
        TopicId: string;
        IsPass: boolean;
        Comment: string;
        PhotoURL: string;
    }
    
    //Service requirment to call api
    export class CheckedRequest {
        constructor(public id: string) { };
    }
    
    //Service requirment to call api
    export class GetReadyStatusRequest {
        constructor(public id: string) { };
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