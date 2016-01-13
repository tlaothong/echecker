module app {
    'use strict';
    
    //Data information for vehicleNotification
    export class VehicleNotificationInformation {
        id: string;
        PlateNumber: string;
        Province: string;
        CreateDate: Date;
        Email: string;
        LatestCheckedDate: Date;
        VehicleTypeId: number;
        FormId: number;
        PBRDate: Date;
        IsPBRActive: boolean;
        DrivingLicenseDate: Date;
        IsDrivingLicenseActive: boolean;
        CheckDate: Date;
        IsCheckActive: boolean;
        TaxDate: Date;
        IsTaxActive: boolean;
        PayDate: Date;
        IsPayActive: boolean;
    }
}