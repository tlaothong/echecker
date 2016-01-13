module app {
    'use strict';
    
    export class UserInformation {
        Email: string;
    }

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
}