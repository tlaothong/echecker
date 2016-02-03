module app {
    'use strict';
    
    export class TopicInformation {
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
}