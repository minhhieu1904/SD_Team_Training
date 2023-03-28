import { Injectable } from "@angular/core";
import { LocalStorageConstant } from "../_core/constants/localStorge.constants";
import { RoleInformation } from '../_core/models/auth/auth'
import { INavData } from '@coreui/angular';
import { RolesConstants } from "../_core/constants/role.constants";


@Injectable({ providedIn: 'root'})
export class Nav {
    userRoles: RoleInformation[] = JSON.parse(localStorage.getItem(LocalStorageConstant.Role)!) || [];
    navItems: INavData[] = [];
    constructor(){ }

    getNav(){
        let roles = this.userRoles.map(x => x.unique.trim()).filter(x => RolesConstants.map(r => r.children.filter(c => c.role == x)));
        let rolesConstants = RolesConstants;
        this.navItems = [];
        rolesConstants.forEach(nav => {
            let navItem: INavData = { ...nav, children: [] };
            nav.children.forEach(childNav => {
                if(roles.includes(childNav.role))
                    navItem.children!.push(childNav)
            });
            this.navItems.push(navItem);
        });
        return this.navItems;
    }
}