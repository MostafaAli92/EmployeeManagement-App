import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { EmployeeListComponent } from "./employee-list/employee-list.component";
import { LoginComponent } from "./login/login.component";
import { AuthGuard } from "./guards/auth.guard";

const appRoutes: Routes = [
    { path: '', component: LoginComponent },
    { path: 'login', component: LoginComponent },
    { path: 'employee-list', component: EmployeeListComponent, canActivate: [AuthGuard]}
]


@NgModule({
    imports: [
        RouterModule.forRoot(appRoutes),
      ],
      exports: [ RouterModule ]
})

export class AppRoutingModule{}
