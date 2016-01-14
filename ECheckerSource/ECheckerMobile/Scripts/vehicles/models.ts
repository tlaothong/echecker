module app {
    'use strict';

    //Data information for vehicle
    export class VehicleInformation {
        id: string;
        PlateNumber: string;
        Province: string;
        CreateDate: Date;
        Email: string;
        VehicleProgress: number;
        StatusCode: number;
        VehicleTypeId: number;
        LatestCheckedDate: Date;
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