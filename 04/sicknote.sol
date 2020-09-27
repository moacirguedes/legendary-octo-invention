pragma solidity >=0.4.16 <0.7.0;

contract SickNote {
    string private patient;
    string private diagnosis;
    string private date;

    constructor(string memory patient, string memory diagnosis, string memory date) public {
        patient = patient;
        diagnosis = diagnosis;
        date = date;
    }

    function update(string memory newPatient, string memory newDiagnosis, string memory newDate) public {
        patient = newPatient;
        diagnosis = newDiagnosis;
        date = newDate;
    }
    
    function getPatient() public view returns (string memory) {
        return patient;
    }
    
    function getDiagnosis() public view returns (string memory) {
        return diagnosis;
    }
    
    function getDate() public view returns (string memory) {
        return date;
    }
}
