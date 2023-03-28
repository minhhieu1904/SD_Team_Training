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
    }
]