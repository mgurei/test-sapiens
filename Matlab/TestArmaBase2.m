
Legnati = zeros (5, 30);

for F = 1:5
for Danno = 1:30
    

        
Legnati(F, Danno) = MortiArma2(.5, .5,F,Danno,300, 50 );
end
end



%% plot
plot(Legnati)
