import { inject } from "@angular/core";
import {ActivatedRoute, Router} from "@angular/router";
import { NgxSpinnerService } from "ngx-spinner";
import { NgSnotifyService } from '../services/common/ng-snotify.service'
import { DestroyService } from '../services/common/destroy.service';
import { FunctionUtility } from "./function-utility";
export abstract class InjectBase {
    protected router = inject(Router);
    protected route = inject(ActivatedRoute);
    protected spinnerService = inject(NgxSpinnerService);
    protected snotifyService = inject(NgSnotifyService);
    protected destroyService = inject(DestroyService);
    protected functionUtility = inject(FunctionUtility);
}
