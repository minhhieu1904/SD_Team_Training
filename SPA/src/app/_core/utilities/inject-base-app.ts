import { inject } from "@angular/core";
import {ActivatedRoute, Router} from "@angular/router";
// import { DestroyService } from "@services/common/destroy.service";
// import { NgSnotifyService } from '@services/common/ng-snotify.service';
<<<<<<< HEAD

=======
>>>>>>> 5e7b9dd (1.3 Department Data Maintenance (Done))
import { FunctionUtility } from "@utilities/function-utility";
import { NgxSpinnerService } from "ngx-spinner";
export abstract class InjectBase {
    protected router = inject(Router);
    protected route = inject(ActivatedRoute);
    protected spinnerService = inject(NgxSpinnerService);
    // protected snotifyService = inject(NgSnotifyService);
    // protected destroyService = inject(DestroyService);
    protected functionUtility = inject(FunctionUtility);
}
