import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { RouterModule, Routes } from '@angular/router';
import { MatMomentDateModule } from '@angular/material-moment-adapter';
import { MatButtonModule, MatIconModule, MatSnackBarModule } from '@angular/material';
import { TranslateModule } from '@ngx-translate/core';
import 'hammerjs';

import { FuseModule } from '@fuse/fuse.module';
import { FuseSharedModule } from '@fuse/shared.module';
import { FuseProgressBarModule, FuseSidebarModule, FuseThemeOptionsModule } from '@fuse/components';

import { fuseConfig } from 'app/fuse-config';

import { AppComponent } from 'app/app.component';
import { LayoutModule } from 'app/layout/layout.module';
// import { SampleModule } from 'app/main/sample/sample.module';
// import { QuestionsModule } from 'app/main/questions/questions.module';
// import { EasyestimateModule } from 'app/main/easyestimate/easyestimate.module';
// import { RefercaseModule } from 'app/main/refercase/refercase.module';
// import { SettingsModule } from 'app/main/settings/settings.module';
// import { MycasesModule } from 'app/main/mycases/mycases.module';


const appRoutes: Routes = [    
    {
        path        : '',
        loadChildren: './main/pages/pages.module#PagesModule'
    }
];

@NgModule({
    declarations: [
        AppComponent,

    ],
    imports     : [
        BrowserModule,
        BrowserAnimationsModule,
        HttpClientModule,
        RouterModule.forRoot(appRoutes),

        TranslateModule.forRoot(),

        // Material moment date module
        MatMomentDateModule,

        // Material
        MatButtonModule,
        MatIconModule,
        MatSnackBarModule,


        // Fuse modules
        FuseModule.forRoot(fuseConfig),
        FuseProgressBarModule,
        FuseSharedModule,
        FuseSidebarModule,
        FuseThemeOptionsModule,

        // App modules
        LayoutModule,
    ],
    bootstrap   : [
        AppComponent
    ]
})
export class AppModule
{
}
