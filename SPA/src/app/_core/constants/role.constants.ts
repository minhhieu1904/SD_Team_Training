export const RolesConstants = [
    {
        name: '1. Maintain',
        url: '/maintain',
        icon: 'fa fa-cogs',
        children: [
            {
                name: '1.1 Shift Data Maintenance',
                role: 'BQRC.ShiftDataMaintenance',
                url: '/maintain/shift-data-maintain'
            },
            {
                name: '1.2 Warehouse Basic Data Maintenance',
                role: 'BQRC.WarehouseBasicDataMaintenance',
                url: '/maintain/warehouse-basic-data'
            },
            {
                name: '1.3 Department Data Maintenance',
                role: 'BQRC.DepartmentDataMaintenance',
                url: '/maintain/department-data-maintenance'
            },
            {
                name: '1.4 Standard Packing Quantity Setting',
                role: 'BQRC.StandardPackingQuantitySetting',
                url: '/maintain/standard-packing-quantity'
            },
            {
                name: '1.5 Authorization Setting',
                role: 'BQRC.AuthorizationSetting',
                url: '/maintain/authorization-setting'
            }
        ]
    },
    {
        name: '2. Transaction', 
        url: '/transaction',
        icon: 'fa fa-newspaper-o', 
        children: [
            {
                name: '2.1 Search For Order Data',
                role: 'BQRC.SearchForOrderData',
                url: '/transaction/search-for-order-data'
            }
        ]
    },
    {
        name: '4. Report', 
        url: '/report',
        icon: 'fa fa-newspaper-o', 
        children: [
            {
                name: '4.1 wksh Sum Report',
                role: 'BQRC.wkshSumReport',
                url: '/report/wksh-sum-report'
            },
            {
                name: '4.2 Sort Sum Report',
                role: 'BQRC.SortSumReport',
                url: '/report/sort-sum-report'
            },
            {
                name: '4.3 Storage Sum Report',
                role: 'BQRC.StorageSumReport',
                url: '/report/storage-sum-report'
            },
            {
                name: '4.4 QRCode WIP Report',
                role: 'BQRC.QRCodeWIPReport',
                url: '/report/qrcode-wip-report'
            } 
        ]
    },
]
export class RolesConstants_role {
    public static ShiftDataMaintenance = "BQRC.ShiftDataMaintenance"
    public static WarehouseBasicDataMaintenance = "BQRC.WarehouseBasicDataMaintenance"
    public static DepartmentDataMaintenance = "BQRC.DepartmentDataMaintenance"
    public static StandardPackingQuantitySetting = "BQRC.StandardPackingQuantitySetting"
    public static AuthorizationSetting = "BQRC.AuthorizationSetting"
    public static wkshSumReport = "BQRC.wkshSumReport"
    public static SortSumReport = "BQRC.SortSumReport"
    public static StorageSumReport = "BQRC.StorageSumReport"
    public static QRCodeWIPReport = "BQRC.QRCodeWIPReport"
}