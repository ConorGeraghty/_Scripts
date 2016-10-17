using UnityEngine;
using System.Collections;

public class Track : MonoBehaviour {
	//	What is my purpose?
	//	You're an array.
	//	Oh. My. God.
	public Transform[] checkpoints;

	public Transform[] GetCheckpoints(){
		return checkpoints;
	}
}

/*

                                                                            
                ```   ./oo:                                                
              .:/:/:`:so/+y+                                               
             .:/:/++/sy+++yy/:///////+++++++oooy`                          
             :/:oo++/ys++++ys+++++++++++++++++o-/                          
             //o+//shyd+//sddm+/++/++++++++/+o..o`                         
            .//+///oyys+//+ooo+++++++++++++++..-/:                         
          `/h+os+//+yyys/+/++++o++++++++++o+..---+                         
        .:+ohooyo++oyooyo++++oyhh+//////+so.-----+.                        
      `//--:hsss+--:ysss/----:////--:::/+-+:-----:/                        
     -o+/////++++++/++/////////////////o:--o------o..-:-`                  
     `+--------------------------------:o--//--/+///:shys`                 
      -/-------------:::::--------------//--o--o::::/hyyh:                 
       ::--------:+syyhhhhhso/-----------o--:+-+++ooohdhm:                 
        +-------+hhhhhhddhdddhhs/--------:+--+:-syyyyydddy                 
        `+-----+dddddhdddddddhddhy:-------+/--o--syyyyydhd/                
         -/--:syhhhhddhyyhhdddddddh/-------o--//-:yyyyyhdhh`               
          ::-sydhyhdNN/--/oshhddhddh:------:+--o--:yyyyyddds               
           +-ohdy.ohNNh/-...+dhddddd:-------+:-:+--/yyyyydhd:              
           `+:ydh+.:sdmNmhyydNmhhddy---------o--+:--syyyyhdhh`             
            -/:ohdy/-/yhyhmmmdhdhmy:---------/+-:o-/./yyyydddo             
             /:-/oyhyyys::shhhhdhy:-----------+:-///  oyyyyddd-            
             `+:--:/oyyhhhhddddho:------------:o--s.  `syyyhdhy`           
              `+------://+ooo+/:---------------//.o    :sssshdhs/          
               -/-------------------------......++d:   +:///+yyyh`         
                ./yyysshyy+-----+++++++oyyyyysssydhh.  ossooshhyy          
                -yyyyyddy:`            `yhhhhhhddhhhy`.yyyyhdmyo.          
               -yyyyyddo`               .yhhhhhhddhhhyyyyyhddy.            
              -yyyyhdd+                  .hhhhhhhmdhdhyyyhdds`             
             .yyyyhdh/                    -hddddhhddyyyyddh+               
            .yyyyhdh-                      :mhhhhddyyyyddh:                
           .yyyyddy.                       `dhhhddyyyyddy.                 
          `syyyddo`                        `dhhdhyyyhddo`                  
         `syyhdh/                    `.`   .dhdhyyyhdh/                    
         ohyhmh:                 `./sddhyo//ddhyyyddds                     
        `+hhmy.               `.+ymNNNNNNNNNmyyyyddddo                     
       `++/+h`             `-ohNNNNNNNNNNNNNmdhhddmhho``                   
   ``.:+///y/           .:sdNNNNNNNNNNNNmyoohyhdmmdhhs////:.```..``        
  +oo++//+so`       `-+ymNNNNNNNNNNNNmy+::/s///sdddhh+:::::/+yhmmdhs+:-.`  
  `-:+oooo-      `:sdNNNNNNNNNNNNNds+::::/o+///syss+:::::/+ymNNNNNNNNNNmo  
       ..     .+ymNNNNNNNNNNNNmds+::://+o+////oy/::::::/sdNNNNNNNNNNNNNNy  
              hNNNNNNNNNNNNmho/:::::/ysooo+/+ss/::::/ohNNNNNNNNNNNNdddNN+  
             :NNNNNNNNNNNmy+/::::::::////+oos+:::/ohmNNNNNNNNNNNNNhodmNm.  
             sNNNNNNNNNNNy://+++++//:::::::::::+ymNNNNNNNNNNNNmddmdmmNN+   
             yNNNNNNNNNNms//::::::/+++++//::/sdNNNNNNNNNNNNNNhohmNmNNh:    
             :mNNNNNNNNNmysssoo++//::::://+dNNNNNNNNNNNNNNmmdddmNNNy-      
              .:+shmmNNNmhhhhyyyssssoo++/:yNNNNNNNNNNNNNhddNmmNNms-        
                    .:+o/`-:+oyhhhhyyyyssyNNNNNNNNNNNNms+dmNNNmo.          
                               `-:+syhhhhdNNNNNNNNNNNddhdmNNd+`            
                                     `-:+yNNNNNNNNNNNmdmNNh/               
                                         .mNNNNNNNNNNNNNy-                 
                                          `:+shmNNNNNmo.                   
                                               `-/oo+`                     
                                                                          -
*/