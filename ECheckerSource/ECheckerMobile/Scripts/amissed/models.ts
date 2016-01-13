module app {
    'use strict';

    //Data information for Amissed
    export class AmissedInformation {
        id: string;
        CheckedId: string;
        VehicleId: string;
        TopicId: string;
        Detail: string;
        DamagePercent: number;
        IsCritical: boolean;
        SuggestTopic: string;
        SuggestDetail: string;
        Comment: string;
        PhotoUrl: string;
        CreateDate: Date;
    }
}