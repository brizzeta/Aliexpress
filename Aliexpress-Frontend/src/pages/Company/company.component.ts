import { Component } from '@angular/core';

@Component({
  selector: 'app-company',
  standalone: true,
  imports: [],
  templateUrl: './company.component.html',
  styleUrls: ['./company.component.scss']
})
export class CompanyComponent {
  companyInfo = {
    description: 'We are a leading e-commerce platform providing a wide range of products across various categories.',
    contact: {
      email: 'support@company.com',
      phone: '+1 234 567 890'
    }
  };
}
