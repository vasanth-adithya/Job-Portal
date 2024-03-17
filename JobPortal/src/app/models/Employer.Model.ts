export class Employer {
    employerId:number; 
    employerName:string;
    userName:string;
    email:string;
    password:string;
    gender:string;
    companyName:string;
    contactPhone:string;
    cwebsiteUrl:string;
    expiresIn: Date;
    role?: string | null;
    token?: string | null;
    resetPasswordExpires?: Date | null;
    
    constructor(
        employerId: number,
        employerName: string,
        userName: string,
        email: string,
        password: string,
        gender: string,
        companyName: string,
        contactPhone: string,
        cwebsiteUrl: string,
        expiresIn: Date,
        role?: string,
        token?: string,
        resetPasswordExpires?: Date,
        
        
    ) {
        this.employerId = employerId;
        this.employerName = employerName;
        this.userName = userName;
        this.email = email;
        this.password = password;
        this.gender = gender;
        this.companyName = companyName;
        this.contactPhone = contactPhone;
        this.cwebsiteUrl = cwebsiteUrl;
        this.expiresIn = expiresIn ;
        this.role = role;
        this.token = token;
        this.resetPasswordExpires = resetPasswordExpires;
        
        
    }
}
