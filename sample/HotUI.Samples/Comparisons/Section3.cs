﻿using System;
using System.Collections.Generic;
using System.Text;


/*
 
import SwiftUI

struct ContentView: View {
    var body: some View {
        VStack(alignment: .leading) {
            Text("Turtle Rock")
                .font(.title)
            HStack {
                Text("Joshua Tree National Park")
                    .font(.subheadline)
                Spacer()
                Text("California")
                    .font(.subheadline)
            }
        }
        .padding()
    }
}

 */

namespace HotUI.Samples.Comparisons
{
    public class Section3 : View
    {
        [Body]
        View body() =>
                 new VStack(alignment: HorizontalAlignment.Leading){
                    new Text("Turtle Rock"),
                    new HStack {
                        new Text("Joshua Tree National Park")
                            .Background(Color.Salmon),
                        new Spacer(),
                        new Text("California")
                            .Background(Color.Green),
                    }
                 }.Padding();

    }
}
