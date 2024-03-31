import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { Employee } from '../shared/employee';
import { StorageService } from '../_services/storage.service';
import { User } from '../shared/user';
import { Router } from '@angular/router';
import { EmployeeService } from '../_services/employee.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-employee-list',
  templateUrl: './employee-list.component.html',
  styleUrls: ['./employee-list.component.css']
})
export class EmployeeListComponent implements OnInit {

  employeesList: Employee[];
  newEmployee:Employee={
    id: 0,
    name: '',
    email: '',
    department: ''
  };

  constructor( private employeeService : EmployeeService, private storageService:StorageService,private route:Router, private toastr:ToastrService ){

  }

  ngOnInit(): void {
   this.loadAllEmployees();
  }

  loadAllEmployees(){
    this.employeeService.getAllEmployees().subscribe(data=>{
      this.employeesList = data;
      console.log(this.employeesList);
     });
  }

  updateEmployee(employee:Employee){
     this.employeeService.updateEmployee(employee)
     .subscribe({
      next: (res) => {
        this.toastr.success('Employee Successfully updated', 'Update Employee');
        this.loadAllEmployees();
      },
      error: (e) => console.error(e)
    });
}

removeEmployee(employeeId:number){
  if(confirm("Are you sure to delete this employee")) {
    this.employeeService.removeEmployee(employeeId)
    .subscribe({
     next: (res) => {
      this.toastr.success("Employee Successfully deleted", "Delete Employee")
       this.loadAllEmployees();
     },
     error: (e) => console.error(e)
   });
  }
}


addEmployee(){
  this.employeeService.addEmployee(this.newEmployee)
  .subscribe({
   next: (res) => {
    this.toastr.success("Employee Successfully added", "Add Employee")
     this.loadAllEmployees();
   },
   error: (e) => console.error(e)
 });
}

logOut(){
  this.storageService.clear();
  this.route.navigate(['/login'])
}



  }

