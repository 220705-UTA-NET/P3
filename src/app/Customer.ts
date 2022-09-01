export interface Customer {
    customerId: string,
    firstName: string,
    lastName: string,
    email: string,
    phone: string,
    password: string
}

export interface AccessToken {
    "Access-Token": string
}