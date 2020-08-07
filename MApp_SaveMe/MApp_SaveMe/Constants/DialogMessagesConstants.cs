using System;
using System.Text;

namespace MApp_SaveMe.Constants
{
    public class DialogMessagesConstants
    {
        public const string Message = "Message";
        public const string OK = "OK";
        public const string Retry = "Connectivity issues. Please try again later";

        // Bad input
        public const string BadInput = "Bad Input. Please check your input again or you have no data yet for this service.";

        // Registration
        public const string RegistrationSuccessful = "Registration successful";
        public const string RegistrationUnsuccessful = "Registration not successful. Please try again later";
        public const string TermsAndCondition = "Please read the consent form and agree to the terms";

        //Daily Expense
        public const string DailyExpenseSuccess_ThresholdReached = "Daily Expense succesfully added. Your maximum threshold for this category has been reached";
        public const string DailyExpenseSuccess_ThresholdNotReached = "Daily Expense succesfully added";
        public const string DailyExpense_Unsuccessful = "Daily Expense could not be added. Please try again later";
        public const string DailyExpense_WeekPlanNotSet = "Please update your weekly expense plan";

        //Reset Expense
        public const string ResetExpense_Successful = "New limits are reset successfully";
        public const string ResetExpense_Unsuccessful = "Could not reset new expenses. Please try again later";

        //Savings
        public const string Savings_Total = "Your total savings : ";
        public const string Savings_Total_0 = "You have not saved any amount yet";

        //Password
        public const string PasswordUpdate_Successful = "Your password has been successfully updated";
        public const string PasswordUpdate_Unsuccessful = "Sorry, could not update password. Please try again later";
        public const string PasswordUpdate_ErrorOne = "Please enter correct password twice";

        //Personal Information
        public const string PersonalInfo_Successful = "Your personal information is successfully updated";
        public const string PersonalInfo_Unsuccessful = "Sorry, could not update your data. Please try again later";

        // Add aditional amount
        public const string AmountExceedError = "The amount you have added is more that the acceptable limit. Please reduce the amount.";
        public const string AddExtra_Successful = "Your additional amount added successfully";

        //Graph
        public const string GraphNoData = "You do not have any savings for the selected time period";

        //Terms
        public const string ConsentTitle = "Informed Consent of Participation";
        public const string Terms = ""  
                                    +"You are invited to participate in the online study Save Me - Personalised Expenditure Control Mobile"
                                    +"Application initiated and conducted by Najma Begum and Prof.Dr.Valentin Schwind.The research is"
                                    +"supervised by Prof.Dr.Matthias Wagner at the University of Applied Sciences in Frankfurt.Please note: \n"                                    
                                    +"Your participation is enitrely voluntary \n"
                                    + "The online study will last approximately 3-5 weeks \n"
                                    + "We will record personal demographics (age, gender, etc.) \n"
                                    + "We may publish our results from this and other sessions in our reports, but all such reports will be "
                                    + "confidential and will neither include your name nor cannot be associated with your identity \n\n"

                                    + "If you have any questions or complaints about the whole informed consent process of this research study or"
                                    +"your rights as a human reserach subject, please contact Prof.Dr.Valentin Schwind (E-Mail:"
                                    +"valentin.schwind @fb2.fra-uas.de) or Prof.Dr.Matthias Wagner.You should carefully read the information"
                                    + " below.Please take as much time as you need to read the consent form.\n\n"

                                    + "1. Purpose and Goal of this Research\n"
                                    + " The purpose of this study is to emphasize the need of daily expense planning by splitting expenses category"
                                    +" wise.The goal is to improve user's savings and budgeting strategies Your participation will help us achieve"
                                    +"this goal.The results of this research may be presented at scientific or professional meetings or published in"
                                    + " scientific proceedings and journals.\n\n"

                                    + "2. Participation and Compensation\n"
                                    + " Your participation in this online study is completely voluntary. You will be one of approximately 6-8 people"
                                    +" being surveyed for this research.You will receive no compensation.You may withdraw and discontinue"
                                    +" participation at any time without penalty.If you decline to participate or withdraw from the online study, no one"
                                    + " on the campus will be told. You may refuse to answer any questions you do not want to answer.\n\n"

                                    + "3. Procedure\n"
                                    + " After confirming your informed consent you will:\n"
                                    + "1. Participant installs the app\n"
                                    + "2. Participant adds the weekly expense plan\n"
                                    + "3. Participant adds everyday expenses on a regular basis\n"
                                    + "4. After one week, the participant updates the expense plan for next week. The participant can check his"
                                    + "savings in the app (overall and category wise) and reset the weekly expense plan appropriately\n"
                                    + "5. The process continues\n"
                                    + "The complete procedure of this online study will last approximately 3-5 weeks.\n\n"

                                    + "4. Risks and Benefits\n"
                                    + "There are no risks associated with this online study. Discomforts or inconveniences will be minor and are not"
                                    +"likely to happen. If any discomforts become a problem, you may discontinue your participation.You will not"
                                    +"directly benefit through participation in this online study. We hope that the information obtained from your"
                                    + "participation may help to bring forward the research in this field.\n\n"

                                    + "5. Data Protection and Confidentiality\n"
                                    + "Personal data (age, gender, etc.) will be recorded while participation.The researcher will not identify you by"
                                    +"your real name in any reports using information obtained from this online study and that your confidentiality as"
                                    +"a participant in this online study will remain secure and encrypted.All data you provide in this online study will"
                                    +"be published anonymized and treated confidentially in compliance with the General Data Protection"
                                    +"Regulation (GDPR) of the European Union (EU). Subsequent uses of records and data will be subject to"
                                    +"standard data use policies which protect the full anonymity of the participating individuals.In all cases, uses of"
                                    +"records and data will be subject to the GDPR.Faculty and administrators from the campus will not have"
                                    +"access to raw data or transcripts.This precaution will prevent your individual comments from having any"
                                    +"negative repercussions. This site uses cookies and other tracking technologies to conduct the research, to"
                                    +"improve the user experience, the ability to interact with the system and to provide additional content from third"
                                    +"parties.Despite careful control of content, the researchers assume no liability for damages, which directly or"
                                    +"indirectly result from the use of this online application. Any recordings cannot be viewed by anyone outside"
                                    +"this research project unless we have you sign a separate permission form allowing us to use them (see"
                                    +"below). Records that have not been made public are automatically deleted after the end of the research.The"
                                    +"records will be destroyed if you contact the researcher to destroy or delete them immediately.As with any"
                                    +"publication or online related activity, the risk of a breach of confidentiality is always possible. According to the"
                                    + "GDPR, the researchers will inform the participant if a breach of confidential data was detected.\n\n"

                                    + "6. Identification of Investigators\n"
                                    + "If you have any questions or concerns about the research, please feel free to contact:\n"

                                    + "Ms.Najma Begum"
                                    +" - Student"
                                    +" - najma.begum @stud.frauas.de\n\n"
                                    
                                    + "Prof. Dr.Valentin Schwind\n"
                                    + "Principal Investigator"
                                    +" - Human-Computer Interaction"
                                    +" - Nibelungenplatz 1"
                                    +" - 60318 Frankfurt a. M., Germany"
                                    + " - valentin.schwind @fb2.fra-uas.de\n\n"

                                    + "Prof. Dr.Matthias Wagner\n"
                                    + " - Professor/Head of Department"
                                    +" - High Integrity Systems"
                                    +" - Nibelungenplatz 1"
                                    +" - 60318 Frankfurt a. M., Germany";
    }
}
