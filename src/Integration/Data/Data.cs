namespace Infrastructure.Data;

public static class Data
{
    public const string JSON = """
{
  "CEO": {
    "ID": "1",
    "Name": "John",
    "Surname": "Doe",
    "Jobtitle": "CEO"
  },
  "Departments": {
    "IT": {
      "TopManager": {
        "ID": "2",
        "Name": "Alice",
        "Surname": "Smith",
        "Jobtitle": "IT Manager"
      },
      "Teams": {
        "Software Development": {
          "TeamManager": {
            "ID": "3",
            "Name": "Bob",
            "Surname": "Johnson",
            "Jobtitle": "Software Engineering Manager"
          },
          "Employees": [
            {
              "ID": "4",
              "Name": "Eva",
              "Surname": "Brown",
              "Jobtitle": "Software Engineer"
            },
            {
              "ID": "5",
              "Name": "Daniel",
              "Surname": "Garcia",
              "Jobtitle": "Software Developer"
            },
            {
              "ID": "6",
              "Name": "Sophia",
              "Surname": "Martinez",
              "Jobtitle": "Front-end Developer"
            }
          ]
        },
        "Infrastructure": {
          "TeamManager": {
            "ID": "7",
            "Name": "Charlie",
            "Surname": "Davis",
            "Jobtitle": "Infrastructure Manager"
          },
          "Employees": [
            {
              "ID": "8",
              "Name": "Grace",
              "Surname": "White",
              "Jobtitle": "Network Administrator"
            },
            {
              "ID": "9",
              "Name": "Liam",
              "Surname": "Taylor",
              "Jobtitle": "Systems Administrator"
            },
            {
              "ID": "10",
              "Name": "Olivia",
              "Surname": "Anderson",
              "Jobtitle": "Database Administrator"
            }
          ]
        },
        "Administration": {
          "TeamManager": {
            "ID": "11",
            "Name": "Emma",
            "Surname": "Clark",
            "Jobtitle": "Administration Manager"
          },
          "Employees": [
            {
              "ID": "12",
              "Name": "Sophie",
              "Surname": "Harris",
              "Jobtitle": "Admin Assistant"
            },
            {
              "ID": "13",
              "Name": "Jack",
              "Surname": "Allen",
              "Jobtitle": "Office Coordinator"
            }
          ]
        }
      }
    },
    "Marketing": {
      "TopManager": {
        "ID": "14",
        "Name": "David",
        "Surname": "Wilson",
        "Jobtitle": "Marketing Manager"
      },
      "Teams": {
        "Digital Marketing": {
          "TeamManager": {
            "ID": "15",
            "Name": "Frank",
            "Surname": "Taylor",
            "Jobtitle": "Digital Marketing Manager"
          },
          "Employees": [
            {
              "ID": "16",
              "Name": "Emily",
              "Surname": "Moore",
              "Jobtitle": "Digital Marketing Specialist"
            },
            {
              "ID": "17",
              "Name": "Hannah",
              "Surname": "Johnson",
              "Jobtitle": "SEO Analyst"
            },
            {
              "ID": "18",
              "Name": "George",
              "Surname": "Davis",
              "Jobtitle": "Content Strategist"
            }
          ]
        },
        "Content": {
          "TeamManager": {
            "ID": "19",
            "Name": "Eva",
            "Surname": "Brown",
            "Jobtitle": "Content Manager"
          },
          "Employees": [
            {
              "ID": "20",
              "Name": "Isaac",
              "Surname": "Smith",
              "Jobtitle": "Content Writer"
            },
            {
              "ID": "21",
              "Name": "Chloe",
              "Surname": "Garcia",
              "Jobtitle": "Graphic Designer"
            },
            {
              "ID": "22",
              "Name": "Ava",
              "Surname": "Martinez",
              "Jobtitle": "Video Editor"
            }
          ]
        },
        "Administration": {
          "TeamManager": {
            "ID": "23",
            "Name": "Liam",
            "Surname": "Moore",
            "Jobtitle": "Administration Manager"
          },
          "Employees": [
            {
              "ID": "24",
              "Name": "Noah",
              "Surname": "Allen",
              "Jobtitle": "Admin Assistant"
            },
            {
              "ID": "25",
              "Name": "Mia",
              "Surname": "Harris",
              "Jobtitle": "Office Coordinator"
            }
          ]
        }
      }
    },
    "Sales": {
      "TopManager": {
        "ID": "26",
        "Name": "Olivia",
        "Surname": "Martinez",
        "Jobtitle": "Sales Manager"
      },
      "Teams": {
        "Sales Team 1": {
          "TeamManager": {
            "ID": "27",
            "Name": "Liam",
            "Surname": "Garcia",
            "Jobtitle": "Sales Team 1 Manager"
          },
          "Employees": [
            {
              "ID": "28",
              "Name": "Noah",
              "Surname": "Smith",
              "Jobtitle": "Sales Representative"
            },
            {
              "ID": "29",
              "Name": "Mia",
              "Surname": "Wilson",
              "Jobtitle": "Account Executive"
            },
            {
              "ID": "30",
              "Name": "Mason",
              "Surname": "Davis",
              "Jobtitle": "Sales Analyst"
            }
          ]
        },
        "Sales Team 2": {
          "TeamManager": {
            "ID": "31",
            "Name": "Sophia",
            "Surname": "Taylor",
            "Jobtitle": "Sales Team 2 Manager"
          },
          "Employees": [
            {
              "ID": "32",
              "Name": "Lily",
              "Surname": "Brown",
              "Jobtitle": "Sales Associate"
            },
            {
              "ID": "33",
              "Name": "Aiden",
              "Surname": "Anderson",
              "Jobtitle": "Sales Consultant"
            },
            {
              "ID": "34",
              "Name": "Ella",
              "Surname": "Moore",
              "Jobtitle": "Sales Support"
            }
          ]
        },
        "Administration": {
          "TeamManager": {
            "ID": "35",
            "Name": "Jack",
            "Surname": "Clark",
            "Jobtitle": "Administration Manager"
          },
          "Employees": [
            {
              "ID": "36",
              "Name": "Sophie",
              "Surname": "Harris",
              "Jobtitle": "Admin Assistant"
            },
            {
              "ID": "37",
              "Name": "Oliver",
              "Surname": "Smith",
              "Jobtitle": "Office Coordinator"
            }
          ]
        }
      }
    },
    "Accounting": {
      "TopManager": {
        "ID": "38",
        "Name": "Mia",
        "Surname": "Johnson",
        "Jobtitle": "Accounting Manager"
      },
      "Teams": {
        "Finance": {
          "TeamManager": {
            "ID": "39",
            "Name": "Noah",
            "Surname": "Lee",
            "Jobtitle": "Finance Manager"
          },
          "Employees": [
            {
              "ID": "40",
              "Name": "Chloe",
              "Surname": "Taylor",
              "Jobtitle": "Financial Analyst"
            },
            {
              "ID": "41",
              "Name": "Ava",
              "Surname": "Garcia",
              "Jobtitle": "Tax Specialist"
            },
            {
              "ID": "42",
              "Name": "Liam",
              "Surname": "Smith",
              "Jobtitle": "Audit Assistant"
            }
          ]
        },
        "Billing": {
          "TeamManager": {
            "ID": "43",
            "Name": "Mason",
            "Surname": "Brown",
            "Jobtitle": "Billing Manager"
          },
          "Employees": [
            {
              "ID": "44",
              "Name": "Lily",
              "Surname": "Smith",
              "Jobtitle": "Billing Specialist"
            },
            {
              "ID": "45",
              "Name": "Aiden",
              "Surname": "Davis",
              "Jobtitle": "Billing Coordinator"
            },
            {
              "ID": "46",
              "Name": "Ella",
              "Surname": "Wilson",
              "Jobtitle": "Billing Analyst"
            }
          ]
        },
        "Administration": {
          "TeamManager": {
            "ID": "47",
            "Name": "Emma",
            "Surname": "Anderson",
            "Jobtitle": "Administration Manager"
          },
          "Employees": [
            {
              "ID": "48",
              "Name": "Sophie",
              "Surname": "Taylor",
              "Jobtitle": "Admin Assistant"
            },
            {
              "ID": "49",
              "Name": "Jack",
              "Surname": "Harris",
              "Jobtitle": "Office Coordinator"
            }
          ]
        }
      }
    }
  }
}
""";
}
