import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { TranslateModule } from '@ngx-translate/core';

import { FuseSharedModule } from '@fuse/shared.module';

import { SettingsComponent } from './settings.component';

const routes = [
    {
        path     : 'settings',
        component: SettingsComponent
    }
];

@NgModule({
    declarations: [
        SettingsComponent
    ],
    imports     : [
        RouterModule.forChild(routes),

        TranslateModule,

        FuseSharedModule
    ],
    exports     : [
        SettingsComponent
    ]
})

export class SettingsModule
{
}
