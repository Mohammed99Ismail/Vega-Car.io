export interface Contact{
    name:string,
    phone:string,
    email:string
}
export interface KeyPair{
    id:number,
    name:string
}
export interface Vehicle{
    id:number,
    make:KeyPair,
    model:KeyPair,
    isRegistered:string,
    features:KeyPair[],
    lastUpdate:string,
    contact:Contact
}
export interface SaveVehicle{
    id:number,
    makeId:number,
    modelId:number,
    isRegistered:string,
    features:number[],
    contact:Contact
}