
    //CharMode����  
    //����ĳ���ַ���������һ��.  
    function CharMode(iN){  
        if (iN>=48 && iN <=57) //����  
        return 1;  
        if (iN>=65 && iN <=90) //��д��ĸ  
        return 2;  
        if (iN>=97 && iN <=122) //Сд  
        return 4;  
        else  
        return 8; //�����ַ�  
    }  
    //bitTotal����  
    //�������ǰ���뵱��һ���ж�����ģʽ  
    function bitTotal(num){  
        modes=0;  
        for (i=0;i<4;i++){  
        if (num & 1) modes++;  
        num>>>=1;  
        }  
        return modes;  
    }  
    //checkStrong����  
    //���������ǿ�ȼ���  

    function checkStrong(sPW){  
        if (sPW.length<=4)  
        return 0; //����̫��  
        Modes=0;  
        for (i=0;i<sPW.length;i++){  
        //����ÿһ���ַ������ͳ��һ���ж�����ģʽ.  
        Modes|=CharMode(sPW.charCodeAt(i));  
        }  
        return bitTotal(Modes);  
    }  
